using CinemaApiInfrastructure.Extensions;
using CinemaApiApplication.Extensions;
using CinemaApiInfrastructure.Seeders;
using Microsoft.AspNetCore.Mvc;
using CinemaWebApi.Extensions;
using CinemaWebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });

    options.AddPolicy("AllowAzure", builder =>
    {
        builder.WithOrigins("https://cinema-front.azurewebsites.net")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<CinemaApiSeeder>();
await seeder.Seed();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

if (environment == "Development")
{
    app.UseCors("AllowLocalhost3000");
}
else
{
    app.UseCors("AllowAzure");
}

app.UseAuthorization();

app.MapControllers();

app.Run();

// for CinemaWebApi.Tests
public partial class Program { }