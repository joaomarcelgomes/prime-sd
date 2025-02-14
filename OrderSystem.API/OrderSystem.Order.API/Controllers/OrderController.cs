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
        public async Task<ActionResult<Result<UserViewModel>>> CreateOrder([FromBody] OrderRequest order)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(currentUserId == null || !int.TryParse(currentUserId, out _))
            {
                return BadRequest();
            }

            var result = await _orderService.CreateOrder(order, int.Parse(currentUserId));

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<Result<UserViewModel>> RetrieveAllOrdersByUser()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null || !int.TryParse(currentUserId, out _))
            {
                return BadRequest();
            }

            var result = _orderService.RetrieveAllOrdersByUser(int.Parse(currentUserId));

            if (result.Success == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
