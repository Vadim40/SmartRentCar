using SmartRentCar.Models;

namespace SmartRentCar.Repositories
{
    public interface ICompanyRepository
    {
        public Task<Company> GetCompanyById(int companyId);
    }
}
