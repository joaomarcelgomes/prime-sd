using Microsoft.AspNetCore.Mvc;
using OrderSystem.Order.API.Model;
using OrderSystem.Order.API.Models.DTOs;

namespace OrderSystem.Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public ActionResult Create()
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult<Result<UserViewModel>> Retrieve()
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult<Result<UserViewModel>> Update()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult<Result<UserViewModel>> Delete()
        {
            return Ok();
        }

    }
}
