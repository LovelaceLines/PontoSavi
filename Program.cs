using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

using PontoSavi.API.ServiceFilters;
using PontoSavi.Application.Interfaces;
using PontoSavi.Application.Services;
using PontoSavi.Application.Validators;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;
using PontoSavi.Infra.Data.Migrations;
using PontoSavi.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(configure =>
{
    configure.CreateMap<UserDTO, IdentityUser>().ReverseMap();
    configure.CreateMap<RoleDTO, IdentityRole>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region DI

// TODO - Refactor Dependency Injection Configuration/Environment
IConfiguration configuration = builder.Configuration.GetSection("Jwt");
builder.Services.AddSingleton(configuration);

builder.Services.AddTransient<AuthAndUserExtractionFilter>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<UserValidator>();
builder.Services.AddTransient<PasswordValidator>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IdentityUserRole<string>>();
builder.Services.AddTransient<SignInManager<IdentityUser>>();

builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();

builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();

builder.Services.AddTransient<IAuthService, AuthService>();

#endregion

#region Context

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new AppException("JwtConfig: Issuer is null", HttpStatusCode.InternalServerError),
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? throw new AppException("JwtConfig: Audience is null", HttpStatusCode.InternalServerError),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? throw new AppException("JwtConfig: Secret is null", HttpStatusCode.InternalServerError))),
        };
    }
);

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var helperMigration = new HelperMigration(app.Services);

app.UseExceptionHandler("/api/Error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
