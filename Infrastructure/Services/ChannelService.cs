using System;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Infrastructure.Interfaces;

namespace ChatApi.Infrastructure.Services
{    
    public class ChannelService : IBaseController<ChannelModel>
    {
        private readonly IRepository<ChannelModel> _repository;

        public ChannelService(IRepository<ChannelModel> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(ChannelModel model)
        {
            await _repository.CreateAsync(model);
        }

        public async Task<ChannelModel> ReadById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<object> Read(object filter)
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(ChannelModel model)
        {
            await _repository.UpdateAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model != null)
            {
                await _repository.DeleteAsync(model);
            }
        }
    }
}