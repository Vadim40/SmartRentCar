using Microsoft.AspNetCore.Mvc;
using SmartRentCar.DTO;
using SmartRentCar.Services;

namespace SmartRentCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentContractController : ControllerBase
    {
        private readonly IRentContractService _rentContractService;

        public RentContractController(IRentContractService rentContractService)
        {
            _rentContractService = rentContractService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentContractById([FromRoute] int id)
        {
            try
            {
                await _rentContractService.DeleteRentContractById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetRentContractsActive([FromRoute] int statusId)
        {
            
            try
            {
                var rentContracts = await _rentContractService.GetRentContractsActive();
                return Ok(rentContracts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
          [HttpGet("completed")]
        public async Task<IActionResult> GetRentContractsCompleted([FromRoute] int statusId)
        {
            
            try
            {
                var rentContracts = await _rentContractService.GetRentContractsCompleted();
                return Ok(rentContracts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveRentContract([FromBody] RentContractCreateDTO contractDTO)
        {
            try
            {
                var contractId = await _rentContractService.SaveRentContract(contractDTO);
                return Ok(new { ContractId = contractId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRentContract([FromBody] RentContractUpateDTO contractDTO)
        {
            try
            {
                await _rentContractService.UpdateRentContract(contractDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
