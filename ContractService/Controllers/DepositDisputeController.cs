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
           

            return Ok(dispute);
        }

        [HttpGet("message/{deposiDisputeId}")]
           public async Task<ActionResult<DepositDisputeDTO>> GetDisputeMessage([FromRoute] int deposiDisputeId)
        {
            var disputeMessage = await _depositDisputeService.GetDisputeMessage(deposiDisputeId);
            if (disputeMessage == null)
                return NoContent();

            return Ok(disputeMessage);
        }

        [HttpPost("message/create")]
        public async Task<IActionResult> CreateDisputeMessage([FromBody] DisputeMessageDTO disputeMessage)
        {
            await _depositDisputeService.CreateDisputeMessage(disputeMessage);
            return Ok();
        }
        [HttpPut("message/update")]
        public async Task<IActionResult> UpdateDisputeMessage([FromBody] DisputeMessageDTO disputeMessage)
        {
            await _depositDisputeService.UpdateDisputeMessage(disputeMessage);
            return Ok();
        }
    }
}
