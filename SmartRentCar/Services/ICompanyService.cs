using SmartRentCar.DTO;
using SmartRentCar.Models;

namespace SmartRentCar.Services
{
    public interface ICompanyService
    {
        public Task<CompanyDTO> GetCompanyById(int companyId);
    }
}
