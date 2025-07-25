using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Requests;
using ChatApi.Domain.Responses;
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
            var results = await _repository.FindAsync(m => m.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<UserModel> ReadByMail(string email)
        {
            return await _repository.GetByConditionAsync(c => c.Email == email);
        }

        public async Task<PaginationResponse> Read(UserFilterRequest filter)
        {
            var entities = await _repository.GetAllAsync();
            var filtered = entities.ToList();

            if (filter.Active.HasValue && filter.Active.Value == true)
            {
                filtered = filtered.Where(x => x.ActiveAt.HasValue).ToList();
            }

            var total = filtered.Count;
            var skip = (filter.Page - 1) * filter.PageSize;

            List<UserResponse> paged = filtered
                .Skip(skip)
                .Take(filter.PageSize)
                .Select(entity => new UserResponse(entity))
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return new PaginationResponse
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                Total = total,
                Data = paged
            };
        }

        public async Task UpdateAsync(UserModel model)
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