using ContractService.ContractConfig;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

public class ContractDeploymentService
{
    private readonly Web3 _web3;
    private readonly string _deployerAddress;

    public ContractDeploymentService(Account account, string infuraUrl)
    {
        _web3 = new Web3(account, infuraUrl);
        _deployerAddress = account.Address;
    }

    public async Task<string> DeployContractAsync(string contractJsonPath, params object[] constructorParams)
    {
        var definition = ContractLoader.Load(contractJsonPath);

        try
        {
            Console.WriteLine("🔄 Начинаем деплой контракта...");

            var transactionHash = await _web3.Eth.DeployContract.SendRequestAsync(
                definition.Abi,
                definition.Bytecode,
                _deployerAddress,
                new HexBigInteger(30_000_000),  // газ
                constructorParams               // параметры конструктора
            );

            Console.WriteLine($"📑 Транзакция отправлена, хеш: {transactionHash}");

            // Явное ожидание завершения деплоя
            var receipt = await WaitForTransactionReceipt(transactionHash);

            if (receipt.Status.Value == 1)
            {
                Console.WriteLine($"✅ Контракт успешно задеплоен по адресу: {receipt.ContractAddress}");
                return receipt.ContractAddress;
            }
            else
            {
                Console.WriteLine("❌ Деплой не удался. Статус != 1");
                return "Deployment failed";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"🚨 Ошибка деплоя: {ex.Message}");
            return $"Error: {ex.Message}";
        }
    }

    // Метод для явного ожидания receipt
    private async Task<TransactionReceipt> WaitForTransactionReceipt(string transactionHash)
    {
        TransactionReceipt receipt = null;
        int attempts = 0;
        const int maxAttempts = 60;  // Максимальное количество попыток
        const int delay = 30000;  // Задержка в миллисекундах между попытками (5 секунд)

        while (receipt == null && attempts < maxAttempts)
        {
            attempts++;
            Console.WriteLine($"⏳ Ожидание подтверждения транзакции... Попытка #{attempts}");
            receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            if (receipt == null)
            {
                await Task.Delay(delay);  // Ожидаем перед следующей попыткой
            }
        }

        if (receipt == null)
        {
            Console.WriteLine("❌ Не удалось получить receipt после нескольких попыток.");
        }
        else
        {
            Console.WriteLine($"✅ Транзакция подтверждена, адрес контракта: {receipt.ContractAddress}");
        }

        return receipt;
    }
}
