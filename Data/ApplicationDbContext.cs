using CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = Guid.NewGuid(),
                    Name = "10OFF",
                    Percent = 10,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Coupon
                {
                    Id = Guid.NewGuid(),
                    Name = "20OFF",
                    Percent = 20,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
                );


        }
    }
}
