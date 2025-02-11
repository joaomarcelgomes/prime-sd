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
        public ActionResult<Result<UserViewModel>> Create(UserRequest user)
        {
            var result = _userService.CreateUser(user);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public ActionResult<Result<UserViewModel>> Retrieve(int id)
        {
            var result = _userService.RetrieveUser(id);
            
            if(result.Success == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public ActionResult<Result<UserViewModel>> Update(User userUpdate)
        {
            var result = _userService.UpdateUser(userUpdate);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<Result<UserViewModel>> Delete(int id)
        {
            var result = _userService.DeleteUser(id);

            if (result.Success == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

    }
}
