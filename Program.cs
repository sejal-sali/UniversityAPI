using Microsoft.EntityFrameworkCore;
using UniversityAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with MySQL
var connectionString = "server=localhost;database=universityapi;user=root;password=";
builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 17))));


// Add Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
