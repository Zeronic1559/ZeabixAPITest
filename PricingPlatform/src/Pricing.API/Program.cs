using Pricing.Domain.Interfaces;
using Pricing.Application;
using Pricing.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// Register repositories
builder.Services.AddSingleton<IJobRepository, InMemoryJobRepository>();
builder.Services.AddSingleton<IRuleRepository, InMemoryRuleRepository>();

// Register pricing rules
builder.Services.AddScoped<IPricingRule>(sp => new WeightRule());
builder.Services.AddScoped<IPricingRule>(sp => new RemoteRule());
builder.Services.AddScoped<IPricingRule>(sp => new TimeRule());

// Register PricingService - will receive the rules via DI
builder.Services.AddScoped<PricingService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();

public partial class Program { }