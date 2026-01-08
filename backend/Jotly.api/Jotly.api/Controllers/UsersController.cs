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

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUser() 
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("getbyId-{Id:int}")]
        public async Task<ActionResult<UserDto>> GetById([FromRoute] int Id) 
        {
            var result = await _userService.GetByIdAsync(Id);
            if(result == null) 
                return NotFound("User not found.");

            return Ok(result);
        }
    }
}
