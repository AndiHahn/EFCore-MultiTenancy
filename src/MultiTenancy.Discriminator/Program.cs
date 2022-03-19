using Microsoft.EntityFrameworkCore;
using MultiTenancy.Discriminator;
using MultiTenancy.Discriminator.Infrastructure;
using MultiTenancy.Discriminator.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Migrate and seed database
var services = builder.Services.BuildServiceProvider();
await using var masterContext = services.GetRequiredService<MasterDbContext>();
await masterContext.Database.MigrateAsync();
await masterContext.SeedAsync();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<TenantMiddleware>();

app.MapControllers();

app.Run();
