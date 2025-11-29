using Api.Middleware;
using Application.Interfaces;
using Application.Services;
using Internship_4_OOP2.Application.Services;
using Internship_4_OOP2.Infrastructure.External;
using Internship_4_OOP2.Infrastructure.Persistence;
using Internship_4_OOP2.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsersDB")));

builder.Services.AddDbContext<CompaniesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CompaniesDB")));



builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("UsersDb")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IExternalImportInfoRepository, ExternalImportInfoRepository>();

builder.Services.AddScoped<UserService>(sp =>
{
    double refLat = 43.508133;
    double refLng = 16.440193;
    return new UserService(sp.GetRequiredService<IUserRepository>(), refLat, refLng);
});

builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<ExternalImportService>();
builder.Services.AddHttpClient<IExternalUserService, ExternalUserService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
