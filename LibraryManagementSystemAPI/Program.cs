using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Context;
using LibraryManagementSystemAPI.Exceptions;
using LibraryManagementSystemAPI.Identity;
using LibraryManagementSystemAPI.Repository;
using LibraryManagementSystemAPI.Seed;
using LibraryManagementSystemAPI.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddControllers();
builder.Services.AddSeedDb();
builder.AddGlobalExceptionHandler();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<BookContext>();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true);
        });
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSeedDb();
}
else
{
    app.UseExceptionHandler();
}

app.AddCoverValidationCheck();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.MapIdentityApi<IdentityUser>();
app.MapLogout();

app.UseAuthorization();

app.MapControllers();

app.Run();
