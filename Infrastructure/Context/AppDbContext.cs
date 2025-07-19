using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Linq;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<ChannelModel> Channels { get; set; }

        public DbSet<TenantModel> Tenants { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

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

            for (int i = 2; i < 52; i++)
            {
                modelBuilder.Entity<UserModel>().HasData(new UserModel
                {
                    Id = i,
                    Guid = Guid.NewGuid(),
                    Name = "Dev",
                    Email = $"dev_{i}@example.com",
                    Password = "dev",
                    NuLogged = 0,
                    NuRefreshed = 0,
                    ActiveAt = DateTime.UtcNow,
                    BlockedAt = null
                });
            }

            for (int i = 1; i < 51; i++)
            {
                modelBuilder.Entity<TenantModel>().HasData(new TenantModel
                {
                    Id = i,
                    Guid = Guid.NewGuid(),
                    Name = "Dev",
                    Database = $"{i}_DevDb",
                });
            }

            for (int i = 1; i < 51; i++)
            {
                modelBuilder.Entity<ChannelModel>().HasData(new ChannelModel
                {
                    Id = i,
                    Guid = Guid.NewGuid(),
                    Name = $"Dev_{i}",
                    TenantId = i,
                });
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}