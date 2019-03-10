using Boyner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Boyner.Data
{
    public class ConfigContext : DbContext
    {
        public ConfigContext(DbContextOptions<ConfigContext> options)
            : base(options)
        {
        }

        public ConfigContext()
        {
        }

        public DbSet<Config> Config { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Config>().HasData(
                new Config
                {
                    Id = 1,
                    ApplicationName = "SERVICE-A",
                    IsActive = true,
                    Value = "Boyner.com.tr",
                    Name = "SiteName",
                    Type = "String"
                },
                new Config
                {
                    Id = 2,
                    ApplicationName = "SERVICE-B",
                    IsActive = true,
                    Value = "1",
                    Name = "IsBasketEnabled",
                    Type = "Boolean"
                },
                new Config
                {
                    Id = 3,
                    ApplicationName = "SERVICE-A",
                    IsActive = false,
                    Value = "50",
                    Name = "MaxItemCount",
                    Type = "Int"
                });
            base.OnModelCreating(modelBuilder);
        }
    }

}
