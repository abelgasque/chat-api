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
    public class ChatMessageService : IBaseController<ChatMessageModel>
    {
        private readonly IRepository<ChatMessageModel> _repository;

        public ChatMessageService(IRepository<ChatMessageModel> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(ChatMessageModel model)
        {
            await _repository.CreateAsync(model);
        }

        public async Task<PaginationResponse> Read(ChatMessageFilterRequest filter)
        {
            var entities = await _repository.GetAllAsync();

            if (filter.ChatId.HasValue)
                entities = entities.Where(e => e.ChatId == filter.ChatId.Value);

            var filtered = entities.ToList();
            var skip = (filter.Page - 1) * filter.PageSize;

            List<ChatMessageModel> paged = filtered
                .OrderByDescending(x => x.Timestamp)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToList();

            return new PaginationResponse
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                Total = filtered.Count,
                Data = paged
            };
        }

        public async Task<ChatMessageModel> ReadById(Guid id)
        {
            var results = await _repository.FindAsync(m => m.Id == id);
            return results.FirstOrDefault();
        }

        public async Task UpdateAsync(ChatMessageModel model)
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