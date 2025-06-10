

namespace ContractService.Services
{
    public interface IBlockchainService
    {
        Task ConfirmStart(string contractAddress);
        Task ConfirmEarlyEnd(string contractAddress);
        Task ConfirmEnd(string contractAddress);
        Task RaiseDispute(string contractAddress);
    }
}