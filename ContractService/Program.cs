using ContractService.Config;
using ContractService.Services;
using Microsoft.EntityFrameworkCore;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

var builder = WebApplication.CreateBuilder(args);


var infuraUrl = $"https://sepolia.infura.io/v3/{Environment.GetEnvironmentVariable("INFURA_API_KEY")}";
var privateKey = Environment.GetEnvironmentVariable("ETH_PRIVATE_KEY");
var account = new Account(privateKey);

builder.Services.AddSingleton(new ContractDeploymentService(account, infuraUrl));
var web3 = new Web3(infuraUrl);

// Получаем баланс кошелька в Wei
var balanceWei = await web3.Eth.GetBalance.SendRequestAsync(account.Address);

// Конвертируем баланс в Ether и выводим его
var balanceEther = Web3.Convert.FromWei(balanceWei.Value);
Console.WriteLine($"Balance: {balanceEther} ETH");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=contracts.db"));
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

