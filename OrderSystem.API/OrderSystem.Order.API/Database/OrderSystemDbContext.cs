using Microsoft.EntityFrameworkCore;
using OrderSystem.Order.API.Models;

namespace OrderSystem.Order.API.Database
{
    public class OrderSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public OrderSystemDbContext(DbContextOptions<OrderSystemDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Order>()
                .HasOne(order => order.User) 
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId); 
        }
    }
}
