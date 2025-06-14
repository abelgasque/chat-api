using Server.Infrastructure.Entities.Models;
using Server.Infrastructure.Entities.DTO;
using Server.Infrastructure.Entities.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Server.Infrastructure.Services
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
            return await _repository.GetByConditionAsync(c => c.Mail == mail);
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
                (string.IsNullOrEmpty(userFilter.Mail) || x.Mail.Contains(userFilter.Mail)));

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