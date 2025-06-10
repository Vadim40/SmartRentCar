using Microsoft.AspNetCore.Mvc;
using ContractService.DTOs;
using ContractService.Services;

namespace ContractService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("{rentalId}")]
        public async Task<ActionResult<RentalDTO>> GetRental([FromRoute] int rentalId)
        {
            var rental = await _rentalService.GetRental(rentalId);
            if (rental == null)
                return NotFound();

            return Ok(rental);
        }

        [HttpGet("statuses")]
        public async Task<ActionResult<List<RentalStatusDTO>>> GetRentalStatuses()
        {
                var statuses = await _rentalService.GetRentalStatuses();
                return Ok(statuses);
        
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<RentalDTO>>> GetRentals([FromQuery] FilterToRents filter)
        {
            //TODO
            var rentals = await _rentalService.GetRentals(filter, "TODO", 1);
            return Ok(rentals);
        }

        [HttpPut("{rentalId}/approve")]
        public async Task<IActionResult> ApproveRental([FromRoute] int rentalId)
        {
            await _rentalService.ApproveRental(rentalId);
            return Ok();
        }

        [HttpPut("{rentalId}/dispute")]
        public async Task<IActionResult> InitiateDispute([FromRoute] int rentalId)
        {
            await _rentalService.InitiateDispute(rentalId);
            return Ok();
        }

        [HttpPut("{rentalId}/confirm-early-end")]
        public async Task<IActionResult> ConfirmEarlyEnd([FromRoute] int rentalId)
        {
            await _rentalService.ConfirmEarlyEnd(rentalId);
            return Ok();
        }

        [HttpPut("{rentalId}/confirm-completion")]
        public async Task<IActionResult> ConfirmCompletion([FromRoute] int rentalId)
        {
            await _rentalService.ConfirmEnd(rentalId);
            return Ok();
        }

        [HttpPut("{rentalId}/send-to-arbitration")]
        public async Task<IActionResult> SendToArbitration([FromRoute] int rentalId)
        {
            await _rentalService.SendToArbitration(rentalId);
            return Ok();
        }

        [HttpPut("{rentalId}/confirm-start")]
        public async Task<IActionResult> ConfirmStart([FromRoute] int rentalId)
        {
            await _rentalService.ConfirmStart(rentalId);
            return Ok();
        }

    }

}
