using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models.DTOs.User;
using OrderSystem.Order.API.Services.Interfaces;

namespace OrderSystem.Order.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private IUserService _userService { get; set; }

        public UserController(IUserService userService) 
        { 
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserRequest user)
        {
            try { 
                var result = await _userService.CreateUser(user);

                if (result.Success == false)
                    return Ok(result);
                return BadRequest(result);
            }
            catch
        {
                return BadRequest(new { success = false, message = "Error ao tentar criar o usu�rio" });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> RetrieveUser()
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (currentUserId == null || !int.TryParse(currentUserId, out _))
                {
                    return BadRequest();
                }

                var result = await _userService.RetrieveUser(int.Parse(currentUserId));
            
                if(result.Success)
                {
                    return Ok(result);
                }

                return NotFound(result);
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = "Error ao tentar carregar os dados do usu�rio" });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateUser(UserRequest userUpdate)
        {
            try
            {  
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (currentUserId == null || !int.TryParse(currentUserId, out _))
                {
                    return BadRequest();
                }

                var result = await _userService.UpdateUser(userUpdate, int.Parse(currentUserId));

                if (result.Success)
                    return Ok(result);

                return BadRequest(result);
             }
            catch(Exception)
            {
                return BadRequest(new { success = false, message = "Error ao tentar atualizar o usu�rio" });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteUser()
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (currentUserId == null || !int.TryParse(currentUserId, out _))
                {
                    return BadRequest();
                }

                var result = await _userService.DeleteUser(int.Parse(currentUserId));

                if (result.Success)
                    return Ok(result);

                return NotFound(result);
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = "Error ao tentar deletar o usu�rio" });
            }
        }

    }
}
