using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities;
using BlogSystem.Presistence.Data.DbContexts;
using BlogSystem.Presistence.Data.SeedData;
using BlogSystem.Presistence.Repositories;
using BlogSystem.Services;
using BlogSystem.Services.MapsterConfig;
using BlogSystem.ServicesAbstraction;
using BlogSystem.Web.Extensions;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            ///

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen();

            //dbContext
            builder.Services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //Identity
            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>();    

            //services
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IPostService, PostService>();

             
            //mapster
            builder.Services.AddMapster();
            MapsterConfig.Configure();

            var app = builder.Build();

           await app.SeedDataAsync(builder.Configuration,app.Environment);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogSystem API v1");
                    c.RoutePrefix = string.Empty;
                });
                //app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
