﻿using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Repository.Extensions;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;
using static Repository.Extensions.DtoMapping;
using Request = DataAccess.Models.Request;

namespace Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly MyDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestRepository(MyDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task Endorse(DtoEndorseRequest dtoEndorseRequest)
        {
            var requestToEndorse = await _context.Requests.FirstOrDefaultAsync(r => r.Id == dtoEndorseRequest.Id);

            if (requestToEndorse == null)
            {
                return;
            }
            requestToEndorse.VehicleId = dtoEndorseRequest.VehicleId;
            requestToEndorse.ItsEndorse = dtoEndorseRequest.ItsEndorse;

            _context.Requests.Attach(requestToEndorse);
            _context.Entry(requestToEndorse).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            Process process = new Process();

            process.RequestId = requestToEndorse.Id;
            process.StateId = 2;
            process.UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            _context.Processes.Add(process);
            await _context.SaveChangesAsync();

        }
        public async Task Approve(DtoApproveRequest dtoApproveRequest)
        {
            var requestToApprove = await _context.Requests.FirstOrDefaultAsync(r => r.Id == dtoApproveRequest.Id);

            if (requestToApprove == null)
            {
                return;
            }

            requestToApprove.ItsApprove = dtoApproveRequest.ItsApprove;

            _context.Requests.Attach(requestToApprove);
            _context.Entry(requestToApprove).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            Process process = new Process();

            process.RequestId = requestToApprove.Id;
            process.StateId = 3;
            process.UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            _context.Processes.Add(process);
            await _context.SaveChangesAsync();
        }

        public async Task<Request> Create(DtoRequestByUser dtoRequest)
        {
            Request request;
            request = dtoRequest.ToRequestByUser();

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            Process process = new Process();

            process.RequestId = request.Id;
            process.StateId = 1;
            process.UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            _context.Processes.Add(process);
            await _context.SaveChangesAsync();

            return request;

        }

        public void Delete(int id)
        {
            var request = _context.Requests.FirstOrDefault(x => x.Id == id);

            if (request != null)
            {
                _context.Requests.Remove(request);
                _context.SaveChanges();
            }
            
        }

        public async Task<List<Request>> GetAll()
        {
            List<Request> requests = await _context.Requests.ToListAsync();

            return requests;
        }

        public async Task<Request> GetById(int id)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);

            if(request == null)
            {
                return null;
            }
            return request;
        }

        public async Task Update(DtoRequest dtoRequest)
        {
            Request updateRequest;
            updateRequest = dtoRequest.ToRequest();

            _context.Requests.Attach(updateRequest);
            _context.Entry(updateRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        
    }
}