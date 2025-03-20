using Microsoft.EntityFrameworkCore;
using SmartRentCar.Config;
using SmartRentCar.Repositories;
using SmartRentCar.Repositories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=smartRentCar.db"));
builder.Services.AddScoped<ICarRepository, CarRepositoryImpl>();
builder.Services.AddScoped<IRentContractRepository, RentContractRepositoryImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepositoryImpl>();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapFallbackToFile("index.html");

app.Run();
