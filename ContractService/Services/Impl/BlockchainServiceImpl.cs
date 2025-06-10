using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Nethereum.Web3;
using Nethereum.Contracts;

namespace ContractService.Services.Impl
{
    public class BlockchainServiceImpl : IBlockchainService
    {
        private readonly Web3 _web3;
        private readonly string _abi;
        private readonly string _accountPrivateKey;
        private readonly string _accountAddress;

        public BlockchainServiceImpl(IConfiguration configuration)
        {
            var ethereumConfig = configuration.GetSection("Ethereum");
            var infuraUrl = ethereumConfig.GetValue<string>("InfuraUrl");
            var abiPath = ethereumConfig.GetValue<string>("ContractABIPath");


            _abi = System.IO.File.ReadAllText(abiPath);


            _accountPrivateKey = ethereumConfig.GetValue<string>("AccountPrivateKey");
            _accountAddress = ethereumConfig.GetValue<string>("AccountAddress");

            var account = new Nethereum.Web3.Accounts.Account(_accountPrivateKey);
            _web3 = new Web3(account, infuraUrl);
        }

        private Contract GetContract(string contractAddress)
        {
            return _web3.Eth.GetContract(_abi, contractAddress);
        }

        public async Task ConfirmStart(string contractAddress)
        {
            var contract = GetContract(contractAddress);
            var function = contract.GetFunction("companyConfirmStart");
            var gas = await function.EstimateGasAsync(_accountAddress);
            var receipt = await function.SendTransactionAndWaitForReceiptAsync(_accountAddress, gas, null, null);
            if (receipt.Status.Value == 0)
            {
                 throw new Exception("Transaction failed");
            }
        }

        public async Task ConfirmEnd(string contractAddress)
        {
            var contract = GetContract(contractAddress);
            var function = contract.GetFunction("companyApproveCompletion");
            var gas = await function.EstimateGasAsync(_accountAddress);
            var receipt = await function.SendTransactionAndWaitForReceiptAsync(_accountAddress, gas, null, null);
             if (receipt.Status.Value == 0)
            {
                 throw new Exception("Transaction failed");
            }
        }

        public async Task ConfirmEarlyEnd(string contractAddress)
        {
            var contract = GetContract(contractAddress);
            var function = contract.GetFunction("companyApproveEarlyTermination");
            var gas = await function.EstimateGasAsync(_accountAddress);
            var receipt = await function.SendTransactionAndWaitForReceiptAsync(_accountAddress, gas, null, null);
             if (receipt.Status.Value == 0)
            {
                 throw new Exception("Transaction failed");
            }
        }

        public async Task RaiseDispute(string contractAddress)
        {
            var contract = GetContract(contractAddress);
            var function = contract.GetFunction("raiseDispute");
            var gas = await function.EstimateGasAsync(_accountAddress);
            var receipt = await function.SendTransactionAndWaitForReceiptAsync(_accountAddress, gas, null, null);
             if (receipt.Status.Value == 0)
            {
                 throw new Exception("Transaction failed");
            }
        }

    }
}
