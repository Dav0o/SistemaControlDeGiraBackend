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
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Repository.Extensions.DtoMapping;

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
            if (user.Email != request.Email)
            {
                return null;
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            string token = CreateToken(user);

            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);

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

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        

        public Task<ActionResult<string>> RefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task Update(DtoCreateUser request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User updateUser;
            updateUser = request.ToUser();

            updateUser.PasswordHash = passwordHash;
            updateUser.PasswordSalt = passwordSalt;

            _context.Entry(updateUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //Methods
        private string CreateToken(User user)
        {
            //Tiene que ir a BD y con un foreach agregar cada uno de los roles como claim
            List<Claim> claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Email, user.Email)

            };


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


    }
}
