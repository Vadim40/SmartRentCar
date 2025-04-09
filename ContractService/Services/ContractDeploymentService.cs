using ContractService.ContractConfig;
using Nethereum.Hex.HexTypes;
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

    public async Task<string> DeployContractAsync(string contractJsonPath, string implementationAddress)
    {
        var definition = ContractLoader.Load(contractJsonPath);

        try
        {
            var receipt = await _web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(
                definition.Abi,                         // ABI контракта
                definition.Bytecode,                    // Байткод контракта
                _deployerAddress,                       // Адрес отправителя (деплойера)
                new HexBigInteger(30_000_000),           // Лимит газа
                new HexBigInteger(0),                   // Сумма эфира (если нужно отправить)
                CancellationToken.None                  // Отмена операции
            );

            if (receipt.Status.Value == 1)
            {
                Console.WriteLine($"Contract deployed at address: {receipt.ContractAddress}");
                var transaction = await _web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(receipt.TransactionHash);
                Console.WriteLine($"Transaction Status: {transaction.TransactionIndex}");

                return receipt.ContractAddress;
            }
            else
            {
                Console.WriteLine($"Contract deployment failed with status: {receipt.Status.Value}");
                var error = receipt.TransactionHash; // Для отладки
                Console.WriteLine($"Transaction hash: {error}");
                return "Deployment failed";
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during contract deployment: {ex.Message}");
            return ex.Message;
        }
    }


}
