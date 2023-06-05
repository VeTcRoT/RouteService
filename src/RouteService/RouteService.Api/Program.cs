using BookingService.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using RouteService.Application;
using RouteService.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
  options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "RouteService",
                    Description = "RouteService Web Api"
                });
});

builder.Services.AddDbContext<RouteServiceDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var routeServiceContext = scope.ServiceProvider.GetRequiredService<RouteServiceDbContext>();
    if (!routeServiceContext.Database.CanConnect())
    {
        try
        {
            routeServiceContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration has failed: {ex.Message}");
        }
    }
}

app.UseSwagger();
app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/swagger/V1/swagger.json", "RouteService");
        });

app.UseCustomExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
