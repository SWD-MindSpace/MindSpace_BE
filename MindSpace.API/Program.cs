using Microsoft.EntityFrameworkCore;
using MindSpace.API.Extensions;
using MindSpace.Application.Extensions;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Infrastructure.Extensions;
using MindSpace.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ====================================
// === Add services to the container
// ====================================

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplications();

// ====================================
// === Build the application
// ====================================

var app = builder.Build();

// ====================================
// === Use Middlewares
// ====================================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Adds identity api endpoints
app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<ApplicationUser>();

app.UseAuthorization();

app.MapControllers();

// ===================================================
// === Create a scope and call the service manually
// ===================================================

using var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
var applicationSeeder = scope.ServiceProvider.GetRequiredService<ApplicationDbContextSeeder>();
var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


try
{
    await applicationDbContext.Database.MigrateAsync();

    await applicationSeeder.SeedAllAsync();
}
catch (Exception ex)
{
    logger.LogError(ex, "Error happens during migrations!");
}
app.Run();