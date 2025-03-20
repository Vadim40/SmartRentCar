using SmartRentCar.Models;

namespace SmartRentCar.Repositories
{
    public interface IRentContractRepository
    {
        public Task<List<RentContract>> GetRentContractsByStatus(int userId, int statusId);

        public Task<int> SaveRentContract(RentContract contract);
        public Task UpdateRentContract (RentContract contract);

        public Task DeleteRentContract (int contractId);
    }
}

