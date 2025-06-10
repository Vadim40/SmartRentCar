using ContractService.Config;
using ContractService.Kafka;
using ContractService.Repositories;
using ContractService.Repositories.Impl;
using ContractService.Services;
using ContractService.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();



builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=contracts.db"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IRentalRepository, RentalRepositoryImpl>();
builder.Services.AddScoped<IDepositDisputeRepository, DepositDisputeRepositoryImpl>();

builder.Services.AddScoped<IRentalService, RentalServiceImpl>();
builder.Services.AddScoped<IDepositDisputeService, DepositDisputeServiceImpl>();

builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

