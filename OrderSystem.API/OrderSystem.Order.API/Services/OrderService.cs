using OrderSystem.Order.API.Infrastructure.Database;
using OrderSystem.Order.API.Infrastructure.ExternalServices;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models.DTOs.Order;
using OrderSystem.Order.API.Services.Interfaces;

namespace OrderSystem.Order.API.Services
{
    public class OrderService : IOrderService
    {
        private OrderSystemDbContext _dbContext;
        private RpcClient _rpcClient;
        public OrderService(OrderSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _rpcClient = new RpcClient("http://localhost:8000/");
        }

        public Result<OrderViewModel> CreateOrder(OrderRequest order)
        {
            Models.Order createdOrder = new Models.Order()
            {
                Price = order.Price,
                Description = order.Description,
                Status = order.Status,
                UserId = order.UserId,
            };

            _dbContext.Orders.Add(createdOrder);
            _dbContext.SaveChanges();

            _rpcClient.Call("ProcessPayment", [createdOrder.Id]);

            return new Result<OrderViewModel>()
            {
                Success = true,
                Message = "Pedido cadastrado com sucesso.",
                Data = new OrderViewModel()
                {
                    Id = createdOrder.Id,
                    Price = order.Price,
                    Description = createdOrder.Description,
                    Status = createdOrder.Status,        
                    UserId = createdOrder.UserId,
                }
            };
        }

        public Result<List<OrderViewModel>> RetrieveAllOrdersByUser(int userId)
        {
            var orders = _dbContext.Orders
                .Where(order => order.UserId == userId)
                .Select(order => new OrderViewModel() {
                    Id = order.Id,
                    Price = order.Price,
                    Description = order.Description,
                    Status = order.Status,  
                    UserId = order.UserId
                })
                .ToList();

            if (orders == null)
            {
                return new Result<List<OrderViewModel>>()
                {
                    Success = true,
                    Message = "Nenhum pedido encontrado.",
                    Data = new List<OrderViewModel>()
                };
            }

            return new Result<List<OrderViewModel>>()
            {
                Success = true,
                Message = "Todos os pedidos do usuário foram retornados.",
                Data = orders
            };
        }
    }
}
