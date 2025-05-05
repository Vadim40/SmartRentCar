using Microsoft.AspNetCore.Mvc;
using ContractService.DTOs;
using ContractService.Services;


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
        public async Task<ActionResult<DepositDisputeDTO>> GetDepositDispute(int depositDisputeId)
        {
            var dispute = await _depositDisputeService.GetDepositDispute(depositDisputeId);
            if (dispute == null)
                return NotFound();

            return Ok(dispute);
        }

        [HttpPost("update/{depositDisputeId}")]
        public async Task<IActionResult> UpdateDisputeStatus(int depositDisputeId, [FromBody] int disputeStatusId)
        {
            await _depositDisputeService.UpdateDisputeStatus(depositDisputeId, disputeStatusId);
            return Ok();
        }
    }
}
