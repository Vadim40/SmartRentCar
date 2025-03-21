using AutoMapper;
using SmartRentCar.DTO;
using SmartRentCar.Repositories;

namespace SmartRentCar.Services.Impl
{
    public class CompanyServiceImpl : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyServiceImpl ( ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<CompanyDTO> GetCompanyById(int companyId)
        {
            try
            {
                var company = await _companyRepository.GetCompanyById(companyId);
                return _mapper.Map<CompanyDTO>(company);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
