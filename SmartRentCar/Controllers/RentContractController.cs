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
        public async Task<IActionResult> DeleteRentContractById(int id)
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

        [HttpGet("status/{statusId}")]
        public async Task<IActionResult> GetRentContractsByStatus(int userId, int statusId)
        {
            //TODO заменить на реальный id из токена
            try
            {
                var rentContracts = await _rentContractService.GetRentContractsByStatus(1, statusId);
                return Ok(rentContracts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveRentContract([FromBody] RentContractDTO contractDTO)
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
        public async Task<IActionResult> UpdateRentContract([FromBody] RentContractDTO contractDTO)
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
