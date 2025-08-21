using Microsoft.EntityFrameworkCore;
using ChatApi.Domain.Entities.Configs;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Infrastructure.Context
{
    public class TenantDbContext : DbContext
    {
        public DbSet<UserTransactionModel> UserTransactions { get; set; }

        public DbSet<BotModel> Bots { get; set; }

        public DbSet<ChatModel> Chats { get; set; }

        public DbSet<ChatMessageModel> ChatMessages { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserTransactionConfig());
            modelBuilder.ApplyConfiguration(new BotModelConfig());
            modelBuilder.ApplyConfiguration(new ChatModelConfig());
            modelBuilder.ApplyConfiguration(new ChatMessageModelConfig());
        }
    }
}