using Jotly.api.DTOs;
using Jotly.api.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Jotly.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto request) 
        {
            var user = await _userService.CreateAsync(request);

            return Ok(user);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUser() 
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }
    }
}
