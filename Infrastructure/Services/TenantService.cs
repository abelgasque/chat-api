using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Infrastructure.Interfaces;
using ChatApi.Domain.Responses;
using ChatApi.Domain.Requests;
using ChatApi.Infrastructure.Context;
using ChatApi.Domain.Entities.Settings;

namespace ChatApi.Infrastructure.Services
{
    public class TenantService : IBaseController<TenantModel>
    {
        private readonly IRepository<TenantModel> _repository;
        private readonly ApplicationSettings _settings;

        public TenantService(
            IRepository<TenantModel> repository,
            IOptions<ApplicationSettings> options
        )
        {
            _repository = repository;
            _settings = options.Value;
        }

        private async Task ApplyMigrationsToTenantAsync(string database)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
            optionsBuilder.UseSqlServer(_settings.GetConnectionString(database));

            using var context = new TenantDbContext(optionsBuilder.Options);
            await context.Database.MigrateAsync();
        }

        public async Task CreateAsync(TenantModel model)
        {
            await ApplyMigrationsToTenantAsync(model.Database);
            await _repository.CreateAsync(model);
        }

        public async Task<TenantModel> ReadById(Guid id)
        {
            var results = await _repository.FindAsync(m => m.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<PaginationResponse> Read(PaginationRequest filter)
        {
            var entities = await _repository.GetAllAsync();
            var filtered = entities.ToList();

            var skip = (filter.Page - 1) * filter.PageSize;

            List<TenantResponse> paged = filtered
                .Skip(skip)
                .Take(filter.PageSize)
                .Select(entity => new TenantResponse(entity))
                .ToList();

            return new PaginationResponse
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                Total = filtered.Count,
                Data = paged
            };
        }

        public async Task UpdateAsync(TenantModel model)
        {
            await _repository.UpdateAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await ReadById(id);
            if (model != null) await _repository.DeleteAsync(model);
        }
    }
}