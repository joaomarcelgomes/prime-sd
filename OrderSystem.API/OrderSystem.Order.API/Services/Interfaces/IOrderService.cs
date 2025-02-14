using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models;
using OrderSystem.Order.API.Models.DTOs.Order;

namespace OrderSystem.Order.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Result<OrderViewModel>> CreateOrder(OrderRequest order, int userId);
        Result<List<OrderViewModel>> RetrieveAllOrdersByUser(int userId);
    }
}
