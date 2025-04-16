using SmartRentCar.Models;

namespace SmartRentCar.Repositories
{
    public interface IRentContractRepository
    {
        public Task<List<RentContract>> GetRentContractsByStatus(int userId, int statusId);

        public Task<List<RentContract>> GetRentContracts(int userId);

        public Task<RentContract> GetRentContract(int contractId);
        public Task<int> SaveRentContract(RentContract contract);
        public Task UpdateRentContract (RentContract contract);

        public Task DeleteRentContractById (int contractId);

        public Task<bool> IsCarInRent(int carId, DateTime startDate, DateTime endDate);
    }
}

