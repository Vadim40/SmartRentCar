using Microsoft.AspNetCore.Mvc;
using ContractService.DTOs;
using ContractService.Services;
using ContractService.Models;


namespace ContractService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositDisputeController : ControllerBase
    {
        private readonly IDepositDisputeService _depositDisputeService;

        public DepositDisputeController(IDepositDisputeService depositDisputeService)
        {
            _depositDisputeService = depositDisputeService;
        }

        [HttpGet("{depositDisputeId}")]
        public async Task<ActionResult<DepositDisputeDTO>> GetDepositDispute([FromRoute] int depositDisputeId)
        {
            var dispute = await _depositDisputeService.GetDepositDispute(depositDisputeId);
            if (dispute == null)
                return NotFound();

            return Ok(dispute);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateDispute([FromBody] DisputeUpdateDTO disputeUpdate)
        {
            await _depositDisputeService.UpdateDispute(disputeUpdate);
            return Ok();
        }

        [HttpGet("rental/{rentalId}")]
        public async Task<ActionResult<DepositDisputeDTO>> GetDepositDisputeByRentalId([FromRoute] int rentalId)
        {
            var dispute = await _depositDisputeService.GetDepositDisputeByRentalId(rentalId);
            if (dispute == null)
                return NotFound();

            return Ok(dispute);
        }
    }
}
