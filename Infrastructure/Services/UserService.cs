using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        private readonly IDistributedCache _cache;
        private readonly IRepository<UserModel> _repository;

        public UserService(IRepository<UserModel> repository, IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task CreateAsync(UserModel model)
        {
            await _repository.CreateAsync(model);
        }

        public async Task CreateLeadAsync(UserLeadRequest model)
        {
            var entity = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = model.Username,
                Email = model.Email,
                Phone = model.Phone,
                NuLogged = 0,
                NuRefreshed = 0,
                ActiveAt = DateTime.UtcNow,
                BlockedAt = null
            };

            await _repository.CreateAsync(entity);
        }

        public async Task<UserModel> ReadById(Guid id)
        {
            string cacheKey = $"user:id:{id}";

            var cached = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                return JsonSerializer.Deserialize<UserModel>(cached);
            }

            var results = await _repository.FindAsync(m => m.Id == id);
            var user = results.FirstOrDefault();

            if (user != null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };

                var json = JsonSerializer.Serialize(user);
                await _cache.SetStringAsync(cacheKey, json, options);
            }

            return user;
        }

        public async Task<UserModel> ReadByMail(string email)
        {
            string cacheKey = $"user:email:{email.ToLower()}";

            var cached = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                return JsonSerializer.Deserialize<UserModel>(cached);
            }

            var user = await _repository.GetByConditionAsync(c => c.Email == email);

            if (user != null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };

                var json = JsonSerializer.Serialize(user);
                await _cache.SetStringAsync(cacheKey, json, options);
            }

            return user;
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
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skip)
                .Take(filter.PageSize)
                .Select(entity => new UserResponse(entity))
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