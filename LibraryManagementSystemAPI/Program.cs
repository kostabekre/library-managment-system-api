using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Context;
using LibraryManagementSystemAPI.Repository;
using LibraryManagementSystemAPI.Seed;
using LibraryManagementSystemAPI.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);
builder.Services.AddValidators();
builder.Services.AddRepositories();
builder.Services.AddCoverValidationValues(builder);
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
