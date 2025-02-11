using OrderSystem.Order.API.Model;
using OrderSystem.Order.API.Models.DTOs;

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
