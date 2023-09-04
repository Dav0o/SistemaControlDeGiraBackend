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
        public Task Update(DtoCreateUser user);

        public Task<User> Delete(int id);

        public Task<object> Login(DtoUser request);

        public Task<User?> UpdateStatus(int id);

        //Queries
        public Task<List<User>> GetAll();

        public Task<User> GetById(int id);

    }
}
