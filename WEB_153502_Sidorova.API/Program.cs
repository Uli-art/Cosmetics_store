﻿using WEB_153502_Sidorova.API.Data;
using Microsoft.EntityFrameworkCore;
using WEB_153502_Sidorova.API.Services.CosmeticsService;
using WEB_153502_Sidorova.API.Services.ShopService;
using WEB_153502_Sidorova.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));

builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<ICosmeticsService, CosmeticsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.Run();
