using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Linq;
using Server.Infrastructure.Entities.Models;

namespace Server.Infrastructure.Entities.Context
{
    public class AppDbContext : DbContext
    {
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
                guid = Guid.NewGuid(),
                Name = "Admin",
                Mail = "admin",
                Password = "admin",
                AuthAttempts = 0,
                ActiveAt = DateTime.UtcNow,
                BlockedAt = null
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}