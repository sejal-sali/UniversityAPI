using Microsoft.EntityFrameworkCore;
using UniversityAPI.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace UniversityAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize the web application builder
            var builder = WebApplication.CreateBuilder(args);

            // Register controller services
            builder.Services.AddControllers();

            // Connection string for the MySQL database
            var connectionString = "server=localhost;database=universityapi;user=root;password=";

            // Setup DbContext with MySQL provider
            builder.Services.AddDbContext<UniversityContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 17))));

            builder.Services.AddDbContext<CourseContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 17))));

            builder.Services.AddDbContext<DepartmentContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 17))));

            // Add Swagger to generate API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure CORS to allow any origin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Build and configure the web application
            var app = builder.Build();

            // Enable Swagger in development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Redirect HTTP to HTTPS
            app.UseHttpsRedirection();

            // Define routing for controllers
            app.MapControllers();

            // Apply CORS policy
            app.UseCors("AllowAll");

            // Start the application
            app.Run();
        }
    }
}
