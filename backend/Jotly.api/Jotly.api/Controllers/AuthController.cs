using Jotly.api.DTOs;
using Jotly.api.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jotly.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<AuthResult>> Login([FromBody] LoginRequest request) 
        {
            var result =  await _authService.LoginAsync(request.Username, request.Password);

            if (!result.IsSuccess) 
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResult>> Register([FromBody] CreateUserDto request) 
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.IsSuccess) 
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
