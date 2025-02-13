using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models;
using OrderSystem.Order.API.Services.Interfaces;
using OrderSystem.Order.API.Services;
using OrderSystem.Order.API.Models.DTOs.Order;
using OrderSystem.Order.API.Models.DTOs.User;

namespace OrderSystem.Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult CreateOrder([FromBody] OrderRequest order)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != order.UserId.ToString() && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            var result = _orderService.CreateOrder(order);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult RetrieveAllOrdersByUser([FromQuery] int userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != userId.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var result = _orderService.RetrieveAllOrdersByUser(userId);

            if (result.Success == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
