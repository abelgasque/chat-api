using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Entities.Configs;
using ChatApi.Domain.Entities.Settings;

namespace ChatApi.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        private readonly ApplicationSettings _settings;

        public DbSet<ChannelModel> Channels { get; set; }

        public DbSet<TenantModel> Tenants { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IOptions<ApplicationSettings> optionsSettings
        ) : base(options)
        {
            _settings = optionsSettings.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ChannelModelConfig());
            modelBuilder.ApplyConfiguration(new TenantModelConfig());
            modelBuilder.ApplyConfiguration(new UserModelConfig());

            Guid tenantId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                Id = userId,
                Name = "Admin",
                AvatarUrl = "https://github.com/abelgasque.png",
                Email = "admin@example.com",
                Phone = "11111111111",
                Password = "admin",
                NuLogged = 0,
                NuRefreshed = 0,
                ActiveAt = DateTime.UtcNow,
                BlockedAt = null
            });

            modelBuilder.Entity<TenantModel>().HasData(new TenantModel
            {
                Id = tenantId,
                Name = "Default",
                Database = _settings.TenantDb,
            });

            for (int i = 1; i <= 50; i++)
            {
                modelBuilder.Entity<UserModel>().HasData(new UserModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Developer " + i,
                    Email = "dev" + i + "@example.com",
                    Phone = "11111111111",
                    Password = BCrypt.Net.BCrypt.HashPassword("dev"),
                    NuLogged = 0,
                    NuRefreshed = 0,
                    ActiveAt = DateTime.UtcNow,
                    BlockedAt = null
                });
            }
        }
    }
}