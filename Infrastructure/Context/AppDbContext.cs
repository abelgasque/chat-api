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

            base.OnModelCreating(modelBuilder);
        }
    }
}