using Microsoft.AspNetCore.Identity.Data;
using OrderSystem.Order.API.Models.DTOs;
using LoginRequest = OrderSystem.Order.API.Models.DTOs.LoginRequest;

namespace OrderSystem.Order.API.Services.Interfaces
{
    public interface IAuthService
    {
        Result<string> Login(LoginRequest login);
    }
}
