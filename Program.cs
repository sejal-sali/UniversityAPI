using Microsoft.EntityFrameworkCore;
using UniversityAPI.Context;
using MySql.Data.MySqlClient;

namespace UniversityAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            var connectionString = "server=localhost;database=universityapi;user=root;password=";

            builder.Services.AddDbContext<UniversityContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 17))));

            builder.Services.AddDbContext<CourseContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 17))));

            builder.Services.AddDbContext<DepartmentContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 17))));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS configuration before building the application
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapControllers();
            app.UseCors("AllowAll");
            app.Run();
        }
    }
}
