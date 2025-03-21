using SmartRentCar.DTO;

namespace SmartRentCar.Services
{
    public interface IRentContractService
    {
        public Task<List<RentContractDTO>> GetRentContractsByStatus(int userId, int statusId);

        public Task<int> SaveRentContract(RentContractDTO contract);
        public Task UpdateRentContract(RentContractDTO contract);

        public Task DeleteRentContractById(int contractId);
    }
}
