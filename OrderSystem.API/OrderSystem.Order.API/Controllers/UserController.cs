using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models.DTOs.User;
using OrderSystem.Order.API.Services.Interfaces;

namespace OrderSystem.Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService { get; set; }

        public UserController(IUserService userService) 
        { 
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<Result<UserViewModel>>> CreateUser([FromBody] UserRequest user)
        {
            var result = await _userService.CreateUser(user);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Result<UserViewModel>>> RetrieveUser()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null || !int.TryParse(currentUserId, out _))
            {
                return BadRequest();
            }

            var result = await _userService.RetrieveUser(int.Parse(currentUserId));
            
            if(result.Success == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Result<UserViewModel>>> UpdateUser(UserRequest userUpdate)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null || !int.TryParse(currentUserId, out _))
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUser(userUpdate, int.Parse(currentUserId));

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<Result<UserViewModel>>> DeleteUser()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null || !int.TryParse(currentUserId, out _))
            {
                return BadRequest();
            }

            var result = await _userService.DeleteUser(int.Parse(currentUserId));

            if (result.Success == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

    }
}
