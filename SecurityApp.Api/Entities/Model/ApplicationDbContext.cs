using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecurityApp.Api.Entities.Settings;
using System.Reflection;
using System;
using System.Linq;

namespace SecurityApp.Api.Entities.Model
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ApplicationSettings _settings;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<ApplicationSettings> settings
        ) : base(options)
        {
            _settings = settings.Value;
        }

        public DbSet<CustomerEntity> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.GetConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

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
            base.OnModelCreating(modelBuilder);
        }
    }
}
