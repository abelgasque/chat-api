using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Domain.Requests;
using ChatApi.Domain.Responses;
using ChatApi.Infrastructure.Interfaces;

namespace ChatApi.Infrastructure.Services
{
    public class UserTransactionService : IBaseController<UserTransactionModel>
    {
        private readonly IRepository<UserTransactionModel> _repository;

        public UserTransactionService(IRepository<UserTransactionModel> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(UserTransactionModel model)
        {
            await _repository.CreateAsync(model);
        }

        public async Task<UserTransactionModel> ReadById(Guid id)
        {

            var results = await _repository.FindAsync(m => m.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<PaginationResponse> Read(PaginationRequest filter)
        {
            var entities = await _repository.GetAllAsync();
            var filtered = entities.ToList();

            var total = filtered.Count;
            var skip = (filter.Page - 1) * filter.PageSize;

            List<UserTransactionModel> paged = filtered
                .OrderByDescending(x => x.PaymentAt)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToList();

            return new PaginationResponse
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                Total = total,
                Data = paged
            };
        }

        public async Task UpdateAsync(UserTransactionModel model)
        {
            await _repository.UpdateAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await ReadById(id);
            if (model != null)
            {
                await _repository.DeleteAsync(model);
            }
        }
    }
}