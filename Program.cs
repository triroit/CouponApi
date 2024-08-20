
using CouponApi.Data;
using CouponApi.Models;
using CouponApi.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouponApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            string? connectionString = builder.Configuration.GetConnectionString("Coupons") ?? "Data Source=Coupons.db";

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSqlite<ApplicationDbContext>(connectionString);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/api/coupon", async (ApplicationDbContext _db) =>
            {
                return await _db.Coupons.ToListAsync();
            }).WithName("GetCoupons").Produces<IEnumerable<Coupon>>(200);

            app.MapGet("/api/coupon/{id:Guid}", async (ApplicationDbContext _db, Guid id) =>
            {
                return await _db.Coupons.FirstOrDefaultAsync(u => u.Id == id);
            }).WithName("GetCoupon").Produces<Coupon>(200);

            app.MapPost("/api/coupon", async (ApplicationDbContext _db, [FromBody]CouponCreateDto coupon) =>
            {
                Coupon newCoupon = new Coupon()
                {
                    Id = Guid.NewGuid(),
                    Name = coupon.Name,
                    Percent = coupon.Percent,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                _db.Coupons.Add(newCoupon);
                await _db.SaveChangesAsync();
                return Results.Created("GetCoupon", newCoupon.Id);
            });

            app.MapPut("/api/coupon", (ApplicationDbContext _db, Guid id, CouponUpdateDto coupon) =>
            {

            });

            app.Run();
        }
    }
}
