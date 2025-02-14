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
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserRequest user)
        {
            try { 
                var result = await userService.CreateUser(user);

                if (result.Success == false)
                    return Ok(result);
                return BadRequest(result);
            }
            catch
        {
                return BadRequest(new { success = false, message = "Error ao tentar criar o usuário" });
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

                var result = await userService.RetrieveUser(int.Parse(currentUserId));
            
                if(result.Success)
                {
                    return Ok(result);
                }

                return NotFound(result);
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = "Error ao tentar carregar os dados do usuário" });
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

                var result = await userService.UpdateUser(userUpdate, int.Parse(currentUserId));

                if (result.Success)
                    return Ok(result);

                return BadRequest(result);
             }
            catch(Exception)
            {
                return BadRequest(new { success = false, message = "Error ao tentar atualizar o usuário" });
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

                var result = await userService.DeleteUser(int.Parse(currentUserId));

                if (result.Success)
                    return Ok(result);

                return NotFound(result);
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = "Error ao tentar deletar o usuário" });
            }
        }

    }
}
