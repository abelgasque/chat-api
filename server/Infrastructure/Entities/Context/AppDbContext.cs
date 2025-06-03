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

            //var user = new CustomerModel(Guid.NewGuid() DateTime.Now, null, null, "Admin", "User", null, "admin", "admin", null, 0, true, false, false) { };
            //modelBuilder.Entity<CustomerModel>().HasData(customerAdmin);

            base.OnModelCreating(modelBuilder);
        }
    }
}