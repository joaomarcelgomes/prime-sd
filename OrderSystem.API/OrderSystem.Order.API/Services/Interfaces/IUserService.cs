using OrderSystem.Order.API.Models;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models.DTOs.User;

namespace OrderSystem.Order.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserViewModel>> CreateUser(UserRequest user);
        Task<Result<UserViewModel>> RetrieveUser(int id);
        Task<Result<UserViewModel>> UpdateUser(UserRequest userUpdate, int userId);
        Task<Result<UserViewModel>> DeleteUser(int id);
    }
}
