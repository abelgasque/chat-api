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
            modelBuilder.ApplyConfiguration(new ChannelModelConfig());
            modelBuilder.ApplyConfiguration(new TenantModelConfig());
            modelBuilder.ApplyConfiguration(new UserModelConfig());

            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                Id = 1,
                Guid = Guid.NewGuid(),
                Name = "Admin",
                Email = "admin@example.com",
                Password = "admin",
                NuLogged = 0,
                NuRefreshed = 0,
                ActiveAt = DateTime.UtcNow,
                BlockedAt = null
            });

            modelBuilder.Entity<TenantModel>().HasData(new TenantModel
            {
                Id = 1,
                Guid = Guid.NewGuid(),
                Name = "Default",
                Database = _settings.TenantDb,
            });

            for (int i = 1; i < 51; i++)
            {
                modelBuilder.Entity<ChannelModel>().HasData(new ChannelModel
                {
                    Id = i,
                    Guid = Guid.NewGuid(),
                    Name = $"Dev_{i}",
                    TenantId = 1,
                });
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}