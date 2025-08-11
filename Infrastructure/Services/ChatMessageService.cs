using System;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
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