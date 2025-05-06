using Microsoft.AspNetCore.Mvc;
using ContractService.DTOs;
using ContractService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<RentalDTO>> GetRental(int rentalId)
        {
                var rental = await _rentalService.GetRental(rentalId);
                if (rental == null)
                    return NotFound();

                return Ok(rental);
        }

        [HttpGet("statuses")]
        public async Task<ActionResult<List<RentalStatusDTO>>> GetRentalStatuses()
        {
            try
            {
                var statuses = await _rentalService.GetRentalStatuses();
                return Ok(statuses);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("approve/{rentalId}")]
        public async Task<IActionResult> ApproveRental(int rentalId)
        {
            await _rentalService.ApproveRental(rentalId);
            return Ok();

        }
        [HttpPost("dispute/{rentalId}")]
        public async Task<IActionResult> InitiateDispute(int rentalId)
        {

            await _rentalService.InitiateDispute(rentalId);
            return Ok();

        }

        [HttpPost("filter")]
        public async Task<ActionResult<List<RentalDTO>>> GetRentals([FromBody] FilterToRents filter)
        {

            var rentals = await _rentalService.GetRentals(filter);
            return Ok(rentals);

        }
    }
}
