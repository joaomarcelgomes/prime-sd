using Microsoft.EntityFrameworkCore;
using OrderSystem.Order.API.Model;

namespace OrderSystem.Order.API.Database
{
    public class OrderSystemDbContext : DbContext
    {
        public OrderSystemDbContext(DbContextOptions<OrderSystemDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
