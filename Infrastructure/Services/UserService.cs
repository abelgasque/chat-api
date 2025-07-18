using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Entities.DTO;
using ChatApi.Infrastructure.Repositories;

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

        public async Task<UserModel> ReadByMail(string mail)
        {
            return await _repository.GetByConditionAsync(c => c.Email == mail);
        }

        public async Task<object> Read(object filter)
        {
            var userFilter = filter as UserModel;

            if (userFilter == null)
            {
                throw new ArgumentException("Invalid filter object");
            }

            var result = await _repository.FindAsync(x =>
                (string.IsNullOrEmpty(userFilter.Name) || x.Name.Contains(userFilter.Name)) &&
                (string.IsNullOrEmpty(userFilter.Email) || x.Email.Contains(userFilter.Email)));

            return result;
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