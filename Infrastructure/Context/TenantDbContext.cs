using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Linq;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Domain.Entities.Configs;

namespace ChatApi.Infrastructure.Context
{
    public class TenantDbContext : DbContext
    {
        public DbSet<BotModel> Bot { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BotModelConfig());
        }
    }
}