using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopTI;
using ShopTI.Entities;
using ShopTI.IServices;
using ShopTI.Models;
using ShopTI.Models.Validators;
using ShopTI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUser>, RegisterUserValidator>();
//builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopTI")));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
        .WithOrigins("https://localhost:7177", "http://localhost:5177")
        .AllowAnyHeader()
        .AllowAnyMethod();//.AllowAnyOrigin();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => {
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
