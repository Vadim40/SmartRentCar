using ContractService.Config;
using Microsoft.EntityFrameworkCore;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.IO;

var builder = WebApplication.CreateBuilder(args);


var infuraUrl = $"https://sepolia.infura.io/v3/{Environment.GetEnvironmentVariable("INFURA_API_KEY")}";
var privateKey = Environment.GetEnvironmentVariable("ETH_PRIVATE_KEY");
var account = new Account(privateKey);
var web3 = new Web3(infuraUrl);

var contractDeploymentService = new ContractDeploymentService(account, infuraUrl);


// Деплой контракта фабрики RentContractFactory, передаем адрес реализации
var path = "Hardhat/artifacts/contracts/RentContractFactory.sol/RentContractFactory.json";
var rentContractAddress = "0xa3e451f5462b4484b99d1bdac5a1a73518b435a1";  // Адрес контракта RentContract
var factoryContractAddress = await contractDeploymentService.DeployContractAsync(path, rentContractAddress);
Console.WriteLine($"Deployed RentContractFactory at: {factoryContractAddress}");


var balanceWei = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
var balanceEther = Web3.Convert.FromWei(balanceWei.Value);
Console.WriteLine($"Current balance: {balanceEther} ETH");

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

