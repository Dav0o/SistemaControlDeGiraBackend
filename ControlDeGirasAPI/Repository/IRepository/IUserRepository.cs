using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Internal.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Extensions.DtoMapping;

namespace Repository.IRepository
{
    public interface IUserRepository
    {
        
        //Commands
        public Task<User> Create(DtoCreateUser user);
        public Task Update(DtoUpdateUser user);

        public Task<User> Delete(int id);

        public Task<object> Login(DtoUser request);

        public Task<User?> UpdateStatus(int id);

        public Task<string> ChangePassword(DtoChangePassword request);


       public Task<string> ForgotPassword(DtoForgotPassword request);

       public Task<string> ResetPassword(DtoResetPassword request);

        //Queries
        public Task<List<User>> GetAll();

        public Task<User> GetById(int id);

        public Task<List<User>> GetByRole(String roleName);

        public bool IsUniqueDNI(string dni);

    }
}