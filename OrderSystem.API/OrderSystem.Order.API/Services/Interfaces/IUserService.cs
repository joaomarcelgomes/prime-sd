using OrderSystem.Order.API.Models;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models.DTOs.User;

namespace OrderSystem.Order.API.Services.Interfaces
{
    public interface IUserService
    {
        Result<UserViewModel> CreateUser(UserRequest user);
        Result<UserViewModel> RetrieveUser(int id);
        Result<UserViewModel> UpdateUser(User userUpdate);
        Result<UserViewModel> DeleteUser(int id);
    }
}
