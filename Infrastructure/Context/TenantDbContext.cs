using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Linq;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Infrastructure.Interface;

namespace ChatApi.Infrastructure.Context
{
    public class TenantDbContext : DbContext
    {
        private readonly ITenantService _tenantService;
        public DbSet<BotModel> Bot { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options, ITenantService tenantService)
            : base(options)
        {
            _tenantService = tenantService;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_tenantService.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister =
                    Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType
                    && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}