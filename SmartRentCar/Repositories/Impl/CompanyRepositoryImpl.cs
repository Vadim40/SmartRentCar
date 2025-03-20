using SmartRentCar.Config;
using SmartRentCar.Models;

namespace SmartRentCar.Repositories.Impl
{
    public class CompanyRepositoryImpl : ICompanyRepository
    {
        private readonly ApplicationContext _context;
        public CompanyRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Company> GetCompany(int companyId)
        {
            var company = await _context.Companys.FindAsync(companyId);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with Id {companyId} not found");
            }
            return company;
        }
    }
}
