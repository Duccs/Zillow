using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zillow.Models;

namespace Zillow.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Zillow.Models.Address>? Address { get; set; }
        public DbSet<Zillow.Models.Category>? Category { get; set; }
        public DbSet<Zillow.Models.Contract>? Contract { get; set; }
        public DbSet<Zillow.Models.Customer>? Customer { get; set; }
        public DbSet<Zillow.Models.Image>? Image { get; set; }
        public DbSet<Zillow.Models.RealEstate>? RealEstate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.RealEstates)
                .WithOne(e => e.Category)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RealEstate>()
                .HasMany(i => i.Images)
                .WithOne(r => r.RealEstate)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Address>()
                .HasMany(a => a.RealEstates)
                .WithOne(r => r.Address)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Address>()
                .HasMany(a => a.Customers)
                .WithOne(c => c.Address)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RealEstate>()
                .HasMany(a => a.Contracts)
                .WithOne(c => c.RealEstate)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Customer>()
                .HasMany(a => a.Contracts)
                .WithOne(c => c.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}