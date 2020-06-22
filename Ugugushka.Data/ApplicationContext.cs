using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ugugushka.Data.Models;

namespace Ugugushka.Data
{
    public class ApplicationContext : IdentityDbContext<User>, IDataProtectionKeyContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Partition> Partitions { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<ToyImage> ToyImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderToy> OrderToys { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                Database.Migrate();
            else
                Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entity configuration
            modelBuilder.ApplyConfiguration(new PartitionConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ToyConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderToyConfiguration());
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}
