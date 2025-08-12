using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Entities.Configs;
using ChatApi.Domain.Entities.Settings;
using System.Collections.Generic;

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

            string avatarLink = "https://github.com/{0}.png";
            var users = new List<string>
            {
                "abelgasque","sindresorhus","kamranahmedse","donnemartin","jwasham","996icu","vinta","karpathy","trekhleb",
                "trimstray","getify","visionmedia","c9s","fabpot","weierophinney","springmeyer","dcramer",
                "jeromeetienne","ornicar","davglass","postmodern","tmcw","isaacs","torvalds","fsouza",
                "taylorotwell","yihui","josevalim","kevinsawicki","jordansissel","kripken","sferik","Raynos",
                "Shougo","ekmett","svenfuchs","radar","TooTallNate","dominictarr","davidfowl","torvalds",
                "yyx990803","gustavoguanabara","gaearon","peng-zhihui","charliermarsh","peppy","phodal",
                "dtolnay","GrahamCampbell","freekmurze","Borda"
            };

            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                Id = userId,
                Name = "Admin",
                AvatarUrl = string.Format(avatarLink, users[0]),
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
                    AvatarUrl = string.Format(avatarLink, users[i]),
                    Email = "dev" + i + "@example.com",
                    Phone = "11111111111",
                    Password = "admin",
                    NuLogged = 0,
                    NuRefreshed = 0,
                    ActiveAt = DateTime.UtcNow,
                    BlockedAt = null
                });
            }
        }
    }
}