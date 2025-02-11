using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Order.API.Model;
using OrderSystem.Order.API.Models.DTOs;
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
        public ActionResult<Result<UserViewModel>> Create([FromBody] UserRequest user)
        {
            var result = _userService.CreateUser(user);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<Result<UserViewModel>> Retrieve([FromQuery] int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var result = _userService.RetrieveUser(id);
            
            if(result.Success == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public ActionResult<Result<UserViewModel>> Update([FromBody] User userUpdate)
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != userUpdate.Id.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var result = _userService.UpdateUser(userUpdate);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public ActionResult<Result<UserViewModel>> Delete([FromQuery] int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var result = _userService.DeleteUser(id);

            if (result.Success == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

    }
}
