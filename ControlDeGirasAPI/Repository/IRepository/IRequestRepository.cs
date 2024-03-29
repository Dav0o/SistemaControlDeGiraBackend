﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Extensions.DtoMapping;

namespace Repository.IRepository
{
    public interface IRequestRepository
    {
        //Commands
        public Task<Request> Create(DtoRequestByUser dtoRequest);

        public Task Endorse(DtoEndorseRequest dtoEndorseRequest);

        public Task Approve(DtoApproveRequest dtoApproveRequest);

        public Task Cancel(DtoCanceledRequest dtoCanceledRequest); //new

        public Task Update(DtoRequest dtoRequest);

        public void Delete(int id);
        //Queries

        public Task<List<Request>> GetAll();

        public Task<Request> GetById(int id);

        public Task<List<Request>> GetRequestsByUser(int id);

        public Task<List<Request>> GetRequestsToEndorse();

        public Task<List<Request>> GetRequestsToApprove();

        public Task<List<Request>> GetRequestsVerified();
    }
}
