using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Linq;

namespace SecurityWebApp.Infrastructure.Entities.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CustomerModel> Customer { get; set; }

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

            modelBuilder.Entity<CustomerModel>()
            .HasData(
                new CustomerModel
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    UpdateDate = null,
                    FirstName = "Abel",
                    LastName = "Gasque L. Silva",
                    Mail = "contato.abelgasque@gmail.com",
                    Password = "admin",
                    AuthAttempts = 0,
                    Active = true,
                    Block = false,
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
