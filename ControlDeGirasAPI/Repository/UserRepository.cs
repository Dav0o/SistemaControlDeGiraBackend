using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using Repository.Extensions;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Diagnostics;
using static Repository.Extensions.DtoMapping;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> Create(DtoCreateUser request)
        {
            var isExistsUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (isExistsUser != null)
            {
                return null;
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user;
            user = request.ToUser();

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<object> Login(DtoUser request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Email != request.Email)
            {
                return null;
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            string token = CreateToken(user);


            return token;
        }

        public Task<User> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task Update(DtoUpdateUser request)
        {

            DtoUpdateUser updateUser;
            updateUser = request.ToUserUpdate();

            var current = _context.Users.Find(updateUser.Id);

            _context.Entry(current).CurrentValues.SetValues(updateUser);

            await _context.SaveChangesAsync();
        }

        //Methods
        private string CreateToken(User user)
        {
            var roles = _context.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.Role.Name).ToList();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<User?> UpdateStatus(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.State = !(user.State);
                _context.Users.Attach(user);
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return user;

        }


        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                await _context.Entry(user)
                    .Collection(c => c.user_Roles)
                    .LoadAsync();
            }
            return user;
        }


        public async Task<List<User>> GetByRole(string roleName)
        {
            return await _context.Users
                .Where(u => u.user_Roles.Any(ur => ur.Role.Name == roleName))
                .ToListAsync();
        }

        public async Task<string> ChangePassword(DtoChangePassword request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                DtoUpdatePassword updatePassword = new DtoUpdatePassword();

                updatePassword.PasswordHash = passwordHash;
                updatePassword.PasswordSalt = passwordSalt;

                _context.Entry(user).CurrentValues.SetValues(updatePassword);
                await _context.SaveChangesAsync();

                return "Cambio realizado con exito!";
            }

            return null;



        }


        private string GenerateResetToken()
        {
            var resetToken = Guid.NewGuid().ToString();


            return resetToken;
        }



        public async Task<string> ForgotPassword(DtoForgotPassword request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            var resetToken = GenerateResetToken();

            user.RefreshToken = resetToken;
            user.TokenExpires = DateTime.UtcNow.AddHours(1);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            await SendVerificationEmail(user.Email, resetToken);

            return resetToken;
        }


        public async Task SendVerificationEmail(string userEmail, string resetToken)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("UNA", "prueba070901@gmail.com")); 
                message.To.Add(new MailboxAddress("", userEmail)); 
                message.Subject = "Verificación de cambio de contraseña"; 

                message.Body = new TextPart("html")
                {
                    Text = $"<p>Para cambiar tu contraseña, haz clic en el siguiente enlace:</p><p><a href='http://localhost:5173/ResetPassword?token={resetToken}'>Cambiar Contraseña</a></p>"
                };

                using (var client = new SmtpClient())
                {
                   
                    await client.ConnectAsync("smtp.gmail.com", 587, false);

                    await client.AuthenticateAsync("prueba070901@gmail.com", "xduodhjstaezozyo");

                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo electrónico de verificación.", ex);
            }
        }

        public async Task<string> ResetPassword(DtoResetPassword request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == request.ResetToken);
            if (user == null)
            {
                throw new Exception("Token inválido.");
            }

            if (DateTime.UtcNow > user.TokenExpires)
            {
                throw new Exception("El token ha expirado, solicite uno nuevo.");
            }

            if (request.NewPass != request.ConfirmPassword)
            {
                throw new Exception("Las contraseñas no coinciden.");
            }

            try
            {
                user.PasswordHash = null;
                user.PasswordSalt = null;

                // Crear hash y salt para la nueva contraseña
                CreatePasswordHash(request.NewPass, out byte[] passwordHash, out byte[] passwordSalt);

                DtoUpdatePassword updatePassword = new DtoUpdatePassword();

                updatePassword.PasswordHash = passwordHash;
                updatePassword.PasswordSalt = passwordSalt;


                _context.Entry(user).CurrentValues.SetValues(updatePassword);
                await _context.SaveChangesAsync();



                return "Contraseña restablecida correctamente.";
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al restablecer la contraseña. Error: " + ex.Message);
            }
        }
    }

      
    }
