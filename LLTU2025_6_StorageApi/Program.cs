using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LLTU2025_6_StorageApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext") ?? throw new InvalidOperationException("Connection string 'ApplicationContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
