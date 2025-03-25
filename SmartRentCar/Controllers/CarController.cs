using Microsoft.AspNetCore.Mvc;
using SmartRentCar.DTO;
using SmartRentCar.Services;

namespace SmartRentCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCarById([FromRoute] int id)
        {

            
            try
            {
                var car = await _carService.GetCarById(id);
                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("brands")]
        public async Task<ActionResult> GetCarBrands()
        {
            try
            {
                var carBrands = await _carService.GetCarBrands();
                return Ok(carBrands);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("classes")]
        public async Task<ActionResult> GetCarClasses()
        {
            try
            {
                var carClasses = await _carService.GetCarClasses();
                return Ok(carClasses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("fuel-types")]
        public async Task<ActionResult> GetCarFuelTypes()
        {
            try
            {
                var fuelTypes = await _carService.GetCarFuelTypes();
                return Ok(fuelTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("transmissions")]
        public async Task<ActionResult> GetCarTransmissions()
        {
            try
            {
                var transmissions = await _carService.GetCarTransmissions();
                return Ok(transmissions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetCarsByFilter([FromQuery] FilterToCars filter)
        {
            try
            {
                var cars = await _carService.GetCarsByFilter(filter);
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveCar([FromBody] CarDTO carDTO)
        {
            try
            {
                var carId = await _carService.SaveCar(carDTO);
                return Ok(new { CarId = carId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCar([FromBody] CarDTO carDTO)
        {
            try
            {
                await _carService.UpdateCar(carDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(  ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarById(int id)
        {
            try
            {
                await _carService.DeleteCarById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
