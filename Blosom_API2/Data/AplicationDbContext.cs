using Blossom_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blosom_API2.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
            
        }

        public DbSet<Blossom> Blossoms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Blossom>().HasData(
                new Blossom()
                {
                    Id = 1,
                    Name = "CC Organic Cream",
                    ProductDescrip = "Product 100% natural",
                    Price = 10,
                    Brand = "Vegan World",
                    Stock = 100,
                    ImageUrl = "",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now

                },

                new Blossom()
                {
                    Id = 2,
                    Name = "Essential oil lavender",
                    ProductDescrip = "Product 100% natural",
                    Price = 30,
                    Brand = "Terpenic",
                    Stock = 80,
                    ImageUrl = "",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }

                );
                
        }
    }
}
