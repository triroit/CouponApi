
using AutoMapper;
using CouponApi.Data;
using CouponApi.Models;
using CouponApi.Models.DTO;
using CouponApi.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Linq;
using System.Net;

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
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddScoped<IValidator<CouponCreateDto>, CouponCreateValidation>();
            builder.Services.AddScoped<IValidator<CouponUpdateDto>, CouponUpdateValidation>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/api/coupon", async (ILogger<Program> _logger, ApplicationDbContext _db) =>
            {
                _logger.Log(LogLevel.Information, "Getting all coupons");

                APIResponse response = new();
                response.Result = await _db.Coupons.ToListAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response);
            }).WithName("GetCoupons").Produces<APIResponse>(200);

            app.MapGet("/api/coupon/{id:Guid}", async (ILogger<Program> _logger, ApplicationDbContext _db, Guid id) =>
            {
                _logger.Log(LogLevel.Information, "Getting coupon");
                APIResponse response = new() { Result = new Coupon(), IsSuccess = false, StatusCode = HttpStatusCode.NotFound };
                var coupon = await _db.Coupons.FirstOrDefaultAsync(u => u.Id == id);
                if (coupon == null)
                {
                    response.ErrorMessages.Add("Coupon with provided id doesn't exist.");
                    return Results.NotFound(response);
                }
                response.Result = coupon;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);
                
            }).WithName("GetCoupon").Produces<APIResponse>(200).Produces<APIResponse>(404);

            app.MapPost("/api/coupon", async (ILogger<Program> _logger, ApplicationDbContext _db, 
                IMapper _mapper, IValidator<CouponCreateDto> _validation,
                [FromBody]CouponCreateDto coupon) =>
            {
                _logger.Log(LogLevel.Information, "Creating new coupon");

                APIResponse response = new() { Result = new Coupon(), IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

                var validationResult = await _validation.ValidateAsync(coupon);
                if (!validationResult.IsValid)
                {
                    response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                    return Results.BadRequest(response);
                }
                if (await _db.Coupons.FirstOrDefaultAsync(u => u.Name.ToLower() == coupon.Name.ToLower()) != null)
                {
                    response.ErrorMessages.Add("Coupon with name " + coupon.Name + " is already exist");
                    return Results.BadRequest(response);
                }
                Coupon newCoupon = _mapper.Map<Coupon>(coupon);

                newCoupon.Id = Guid.NewGuid();
                newCoupon.CreatedDate = DateTime.Now;
                newCoupon.UpdatedDate = DateTime.Now;

                _db.Coupons.Add(newCoupon);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Result = newCoupon;
                response.StatusCode = HttpStatusCode.Created;

                return Results.Ok(response);
            }).WithName("CreateCoupon").Produces<APIResponse>(200).Produces<APIResponse>(400);

            app.MapPut("/api/coupon/{id:Guid}", async (ILogger<Program> _logger, 
                ApplicationDbContext _db, IMapper _mapper, 
                IValidator<CouponUpdateDto> _validation,
                Guid id, CouponUpdateDto coupon) =>
            {
                _logger.Log(LogLevel.Information, "Updating coupon");

                APIResponse response = new() { Result = new Coupon(), IsSuccess = false, StatusCode = HttpStatusCode.NotFound };

                var toUpdate = await _db.Coupons.FirstOrDefaultAsync(u=>u.Id==id);
                
                var validationResult = await _validation.ValidateAsync(coupon);
                if(toUpdate == null)
                {
                    response.ErrorMessages.Add("Coupon with provided id doesn't exist.");
                    return Results.NotFound(response);
                }
                if (!validationResult.IsValid)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                    return Results.BadRequest(response);
                }

                toUpdate.Name = coupon.Name;
                toUpdate.Percent = coupon.Percent;
                toUpdate.IsActive = coupon.IsActive;

                _db.Coupons.Update(toUpdate);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Result = toUpdate;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response);
            }).WithName("UpdateCoupon").Produces<APIResponse>(200).Produces<APIResponse>(404).Produces<APIResponse>(400);

            app.MapDelete("api/coupon/{id:Guid}", async (ILogger<Program> _logger, ApplicationDbContext _db, Guid id) =>
            {
                _logger.Log(LogLevel.Information, "Deleting coupon");

                APIResponse response = new() { Result = new Coupon(), IsSuccess = false, StatusCode = HttpStatusCode.NotFound };

                var toDelete = await _db.Coupons.FirstOrDefaultAsync(u => u.Id == id);
                if (toDelete == null)
                {
                    response.ErrorMessages.Add("Coupon with provided id doesn't exist.");
                    return Results.NotFound(response);
                }

                _db.Coupons.Remove(toDelete);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response);
            }).WithName("DeleteCoupon").Produces<APIResponse>(200).Produces<APIResponse>(404);

            app.Run();
        }
    }
}
