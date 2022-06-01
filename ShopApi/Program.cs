using Microsoft.EntityFrameworkCore;
using ShopApi.Extensions;
using ShopApi.Models;
using ShopApi.Models.DBContext;
using ShopApi.Services;
using ShopApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO 1
builder.Services.AddDbContext<ApiContext>(opt =>
    opt.UseInMemoryDatabase("ShopDB"));

builder.Services.AddScoped<IJewleryItemService, JewleryItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
