using SmartRentCar.DTO;

namespace SmartRentCar.Services
{
    public interface IRentContractService
    {
        public Task<List<RentContractDTO>> GetRentContractsByStatus( int statusId);

        public Task<List<RentContractDTO>> GetRentContractsActive();
        public Task<List<RentContractDTO>> GetRentContractsCompleted();

   

        public Task<int> SaveRentContract(RentContractCreateDTO contract);
        public Task UpdateRentContract(RentContractUpateDTO contract);

        public Task DeleteRentContractById(int contractId);
    }
}
