using System;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Infrastructure.Interfaces;

namespace ChatApi.Infrastructure.Services
{    
    public class UserService : IBaseController<UserModel>
    {
        private readonly IRepository<UserModel> _repository;

        public UserService(IRepository<UserModel> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(UserModel model)
        {
            await _repository.CreateAsync(model);
        }

        public async Task<UserModel> ReadById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<UserModel> ReadByMail(string email)
        {
            return await _repository.GetByConditionAsync(c => c.Email == email);
        }

        public async Task<object> Read(object filter)
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(UserModel model)
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