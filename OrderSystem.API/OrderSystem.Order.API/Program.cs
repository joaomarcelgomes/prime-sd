using Microsoft.EntityFrameworkCore;
using OrderSystem.Order.API.Database;
using OrderSystem.Order.API.Services;
using OrderSystem.Order.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<OrderSystemDbContext>(options =>
    options.UseSqlite("Data Source=order-system.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
