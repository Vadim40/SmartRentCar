using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using ContractService.ContractConfig;
using System.Numerics;
using System.IO;

namespace ContractService.Services
{
    public class ContractDeploymentService
    {
        private readonly Web3 _web3;
        private readonly string _deployerAddress;

        public ContractDeploymentService(Account account, string infuraUrl)
        {
            _web3 = new Web3(account, infuraUrl);
            _deployerAddress = account.Address;
        }

        public async Task<string> DeployContractAsync(string contractJsonPath)
        {
            var definition = ContractLoader.Load(contractJsonPath);

            try
            {
                var receipt = await _web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(
                    definition.Abi,
                    definition.Bytecode,
                    _deployerAddress,
                    new Nethereum.Hex.HexTypes.HexBigInteger(9000000)  // Увеличьте лимит газа
                );

                // Логируем информацию о транзакции
                if (receipt.Status.Value == 1)
                {
                    Console.WriteLine($"Contract deployed at address: {receipt.ContractAddress}");
                }
                else
                {
                    Console.WriteLine($"Contract deployment failed with status: {receipt.Status.Value}");
                }
          

            var contractAddress = receipt.ContractAddress;

            // Выводим адрес развернутого контракта в консоль
            Console.WriteLine($"Contract deployed at address: {contractAddress}");

            return contractAddress;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during contract deployment: {ex.Message}");
                return ex.Message;
            }
        }
    }
}
