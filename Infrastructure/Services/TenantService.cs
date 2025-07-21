using System;
using System.Threading.Tasks;
// using Microsoft.Extensions.Configuration;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Infrastructure.Interfaces;
// using ChatApi.Infrastructure.Context;
using System.Linq;
using ChatApi.Domain.Responses;
using ChatApi.Domain.Requests;
using System.Collections.Generic;

namespace ChatApi.Infrastructure.Services
{
    public class TenantService : IBaseController<TenantModel>
    {
        // private readonly IServiceProvider _serviceProvider;
        // private readonly IConfiguration _configuration;
        // private TenantModel _tenant;
        // public string ConnectionString { get; private set; }

        private readonly IRepository<TenantModel> _repository;
        public TenantService(IRepository<TenantModel> repository)
        {
            _repository = repository;
        }

        // public TenantService(IServiceProvider serviceProvider, IConfiguration configuration)
        // {
        //     _serviceProvider = serviceProvider;
        //     _configuration = configuration;
        // }

        // public async Task SetTenantAsync(string tenantId)
        // {
        // using var scope = _serviceProvider.CreateScope();
        // var db = scope.ServiceProvider.GetRequiredService<TenantDbContext>();

        // _tenant = await db.Tenants.FirstOrDefaultAsync(t => t.Guid.ToString() == tenantId);
        // if (_tenant == null)
        //     throw new Exception("Tenant not found");

        // ConnectionString = _configuration.GetConnectionString("TenantTemplate")
        //     .Replace("{DB_NAME}", _tenant.Database);
        // }

        public async Task CreateAsync(TenantModel model)
        {
            await _repository.CreateAsync(model);
        }

        public async Task<TenantModel> ReadById(Guid id)
        {
            var results = await _repository.FindAsync(m => m.Guid == id);
            return results.FirstOrDefault();
        }

        public async Task<PaginationResponse> Read(PaginationRequest filter)
        {
            var entities = await _repository.GetAllAsync();
            var filtered = entities.ToList();

            var skip = (filter.Page - 1) * filter.PageSize;

            List<TenantModel> paged = filtered
                .Skip(skip)
                .Take(filter.PageSize)
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