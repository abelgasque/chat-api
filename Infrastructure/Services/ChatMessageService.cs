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

        public async Task<ChatMessageModel> ReadById(Guid id)
        {
            // var results = await _repository.FindAsync(m => m.Id == id);
            // return results.FirstOrDefault();
            return null;
        }

        public async Task<PaginationResponse> Read(ChatMessageFilterRequest filter)
        {
            // var entities = await _repository.GetAllAsync();
            // var filtered = entities.ToList();

            // if (filter.SenderId != Guid.Empty && filter.ReceiverId != Guid.Empty)
            // {
            //     filtered = filtered.Where(x =>
            //         (x.SenderId == filter.SenderId && x.ReceiverId == filter.ReceiverId) ||
            //         (x.SenderId == filter.ReceiverId && x.ReceiverId == filter.SenderId)
            //     ).ToList();
            // }

            // var skip = (filter.Page - 1) * filter.PageSize;

            // List<ChatMessageModel> paged = filtered
            //     .OrderByDescending(x => x.SentAt)
            //     .Skip(skip)
            //     .Take(filter.PageSize)
            //     .ToList();

            // return new PaginationResponse
            // {
            //     Page = filter.Page,
            //     PageSize = filter.PageSize,
            //     Total = filtered.Count,
            //     Data = paged
            // };
            return null;
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