using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using WebApi.Extension;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureDataAccessServices(builder.Configuration);
            builder.Services.ConfigureBusinessLogicServices(builder.Configuration);
            builder.Services.AddSwaggerConfiguration(builder.Configuration);
            builder.Services.ConfigureJWT(builder.Configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthorization();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.SwaggerConfig(builder.Configuration, "SwaggerConfigTest");
            }
            else if (app.Environment.IsProduction())
            {
                app.SwaggerConfig(builder.Configuration, "SwaggerConfigProd");
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();



            app.Run();
        }
    }
}
