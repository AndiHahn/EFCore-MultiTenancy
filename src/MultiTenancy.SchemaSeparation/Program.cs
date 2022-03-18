using Microsoft.EntityFrameworkCore;
using MultiTenancy.SchemaSeparation;
using MultiTenancy.SchemaSeparation.Infrastructure;
using MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy;
using MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy.TenantAccessor;
using MultiTenancy.SchemaSeparation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration);

// Migrate and seed database
var services = builder.Services.BuildServiceProvider();
await using var masterContext = services.GetRequiredService<MasterDbContext>();
await masterContext.Database.MigrateAsync();
await masterContext.SeedAsync(builder.Configuration);

var tenants = await masterContext.Tenant.ToListAsync();
foreach (var tenant in tenants)
{
    await using var tenantContext = new TenantDbContext(
        new DbContextOptionsBuilder<TenantDbContext>().Options,
        new ManualTenantAccessor(new TenantInfo(tenant.Name, tenant.ConnectionString)));
    tenantContext.Database.Migrate();
    await tenantContext.SeedAsync();
}

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
