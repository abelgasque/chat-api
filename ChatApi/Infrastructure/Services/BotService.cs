using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Domain.Requests;
using ChatApi.Domain.Responses;
using ChatApi.Infrastructure.Interfaces;

namespace ChatApi.Infrastructure.Services
{
    public class BotService : IBaseController<BotModel>
    {
        private readonly IRepository<BotModel> _repository;

        public BotService(IRepository<BotModel> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(BotModel model)
        {
            await _repository.CreateAsync(model);
        }

        public async Task<BotModel> ReadById(Guid id)
        {
            var results = await _repository.FindAsync(m => m.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<PaginationResponse> Read(PaginationRequest filter)
        {
            var entities = await _repository.GetAllAsync();
            var filtered = entities.ToList();

            var skip = (filter.Page - 1) * filter.PageSize;

            List<BotResponse> paged = filtered
                .Skip(skip)
                .Take(filter.PageSize)
                .Select(entity => new BotResponse(entity))
                .ToList();

            return new PaginationResponse
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                Total = filtered.Count,
                Data = paged
            };
        }

        public async Task UpdateAsync(BotModel model)
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