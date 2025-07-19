using System;
using System.Linq;
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
            var results = await _repository.FindAsync(m => m.Guid == id);
            return results.FirstOrDefault();
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
            var model = await ReadById(id);
            if (model != null)
            {
                await _repository.DeleteAsync(model);
            }
        }
    }
}