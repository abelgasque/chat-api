using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Linq;

namespace Server.Infrastructure.Entities.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CustomerModel> Customer { get; set; }

        public DbSet<CustomerRoleModel> CustomerRole { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

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

            var roleAdmin = new CustomerRoleModel(Guid.NewGuid(), DateTime.Now, null, "Administrator", "ROLE_ADMINISTRATOR", true) { };
            var roleCutomer = new CustomerRoleModel(Guid.NewGuid(), DateTime.Now, null, "Customer", "ROLE_CUSTOMER", true) { };
            var customerAdmin = new CustomerModel(Guid.NewGuid(), roleAdmin.Id, DateTime.Now, null, null, "Abel", "Gasque L. Silva", null, "admin", "admin", null, 0, true, false, false) { };

            modelBuilder.Entity<CustomerRoleModel>().HasData(roleAdmin);
            modelBuilder.Entity<CustomerRoleModel>().HasData(roleCutomer);
            modelBuilder.Entity<CustomerModel>().HasData(customerAdmin);

            base.OnModelCreating(modelBuilder);
        }
    }
}
