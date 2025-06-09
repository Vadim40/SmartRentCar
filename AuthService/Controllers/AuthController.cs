using AuthService.DTOs;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _authService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("register/company")]
        public async Task<IActionResult> RegisterCompanyRep([FromBody] RegisterDto dto)
        {
            await _authService.RegisterCompanyRepresentative(dto);
            return Ok();
        }

        [HttpPost("register/arbitrator")]
        public async Task<IActionResult> RegisterArbitrator([FromBody] RegisterDto dto)
        {
            await _authService.RegisterArbitrator(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var authResponse = await _authService.Login(dto);
            return Ok(authResponse);
        }
    }
}
