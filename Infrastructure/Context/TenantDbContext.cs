using Microsoft.EntityFrameworkCore;
using ChatApi.Domain.Entities.Configs;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Infrastructure.Context
{
    public class TenantDbContext : DbContext
    {
        public DbSet<BotModel> Bots { get; set; }

        public DbSet<UserMessageModel> UserMessages { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BotModelConfig());
            modelBuilder.ApplyConfiguration(new UserMessageModelConfig());
        }
    }
}