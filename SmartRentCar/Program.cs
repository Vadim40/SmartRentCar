using Microsoft.EntityFrameworkCore;
using SmartRentCar.Config;
using SmartRentCar.Repositories;
using SmartRentCar.Repositories.Impl;
using SmartRentCar.Services.Impl;
using SmartRentCar.Services;
using SmartRentCar.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
});

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=smartRentCar.db"));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICarRepository, CarRepositoryImpl>();
builder.Services.AddScoped<IRentContractRepository, RentContractRepositoryImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();

builder.Services.AddScoped<ICarService, CarServiceImpl>();
builder.Services.AddScoped<IRentContractService, RentContractServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddSingleton<KafkaProducerService>();
//builder.Services.AddHostedService<KafkaConsumerService>(); 


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
