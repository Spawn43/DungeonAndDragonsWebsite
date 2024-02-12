global using Data.Context;
using Data.Repositories;
using Data.Repositories.Interface;
using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Domain.Mappers.AutoMapper));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ILoginTokenRepository, LoginTokenRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ILoginTokenService, LoginTokenService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ITableService, TableService>();


//builder.Services.AddTransient<ILoginTokenRepository, LoginTokenRepository>();
//builder.Services.AddTransient<IEventRepository, EventRepository>();
//builder.Services.AddTransient<ITableRepository, TableRepository>();
//builder.Services.AddTransient<ITableService, TableService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
