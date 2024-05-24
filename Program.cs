using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Infra.IOC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggerConfiguration(builder.Configuration, builder.Logging);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration.GetSection("Jwt"));
builder.Services.AddDbContextConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration.GetSection("Jwt"));
builder.Services.AddCorsConfiguration(builder.Configuration.GetSection("Cors"));
builder.Services.AddSwaggerConfiguration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddDatabaseAutoMigrationConfiguration();

app.UseExceptionHandler("/api/Error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder.Configuration.GetSection("Cors")["PolicyName"] ??
    throw new AppException("Cors: PolicyName is null!", HttpStatusCode.InternalServerError));

app.UseAuthorization();

app.MapControllers();

app.Run();
