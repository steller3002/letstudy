using FluentValidation;
using Letstudy.Extensions;
using Letstudy.Identity;
using Letstudy.Requests;
using Microsoft.AspNetCore.Authorization;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthorizationHandler, BoardAccessHandler>();
builder.Services.AddServices();

builder.Services.ConfigureRoutes();
builder.Services.ConfigureCors();

var app = builder.Build();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public abstract partial class Program { }