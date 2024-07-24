using LibraryManagementSystemAPI.Context;
using LibraryManagementSystemAPI.Controllers;
using LibraryManagementSystemAPI.CoverValidation;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddRepositories();
builder.Services.AddScoped<ICoverValidation, DefaultCoverValidation>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSeedDb();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSeedDb();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
