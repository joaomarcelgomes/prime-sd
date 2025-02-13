using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Order.API.Models;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models.DTOs.User;
using OrderSystem.Order.API.Services.Interfaces;

namespace OrderSystem.Order.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public ActionResult CreateUser([FromBody] UserRequest user)
    {
        try
        {
            var result = userService.CreateUser(user);

            if (result.Success)
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
    public ActionResult RetrieveUser([FromQuery] int id)
    {
        Console.WriteLine(id);

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        var result = userService.RetrieveUser(id);
        
        if(result.Success == false)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public ActionResult<Result> UpdateUser(int id, [FromBody] UserRequest userUpdate)
    {
        try
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
                return Forbid();

            var result = userService.UpdateUser(userUpdate, id);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        catch(Exception)
        {
            return BadRequest(new { success = false, message = "Error ao tentar atualizar o usuário" });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public ActionResult<Result> DeleteUser(int id)
    {
        try
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
                return Forbid();

            var result = userService.DeleteUser(id);

            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }
        catch(Exception)
        {
            return BadRequest(new { success = false, message = "Error ao tentar deletar o usuário" });
        }
    }

}