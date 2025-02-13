using OrderSystem.Order.API.Models;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models.DTOs.User;

namespace OrderSystem.Order.API.Services.Interfaces
{
    public interface IUserService
    {
        Result CreateUser(UserRequest user);
        Result RetrieveUser(int id);
        Result UpdateUser(UserRequest userUpdate, int id);
        Result DeleteUser(int id);
    }
}
