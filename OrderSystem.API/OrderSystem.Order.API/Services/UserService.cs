using Microsoft.EntityFrameworkCore;
using OrderSystem.Order.API.Database;
using OrderSystem.Order.API.Model;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Services.Interfaces;

namespace OrderSystem.Order.API.Services
{
    public class UserService : IUserService
    {
        private OrderSystemDbContext _dbContext;

        public UserService(OrderSystemDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public Result<UserViewModel> CreateUser(UserRequest user)
        {
            User createdUser = new User() {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            };

            _dbContext.Users.Add(createdUser);
            _dbContext.SaveChanges();

            return new Result<UserViewModel>()
            {
                Success = true,
                Message = "Usuário cadastrado com sucesso",
                Data = new UserViewModel() {
                    Id = createdUser.Id,
                    Email = createdUser.Email,
                    Name = createdUser.Name
                }
            };
        }

        public Result<UserViewModel> RetrieveUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);

            if (user  == null) {
                return new Result<UserViewModel>()
                {
                    Success = false,
                    Message = "Usuário não encontrado",
                    Data = new UserViewModel()
                };
            }

            return new Result<UserViewModel>()
            {
                Success = true,
                Message = "Usuário encontrado",
                Data = new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                }
            };
        }

        public Result<UserViewModel> UpdateUser(User userUpdate)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == userUpdate.Id);

            if (user == null)
            {
                return new Result<UserViewModel>()
                {
                    Success = false,
                    Message = "Usuário não encontrado",
                    Data = new UserViewModel()
                };
            }

            user.Name = userUpdate.Name ?? user.Name;
            user.Email = userUpdate.Email ?? user.Email;
            user.Password = userUpdate.Password ?? user.Password;

            _dbContext.SaveChanges();

            return new Result<UserViewModel>()
            {
                Success = true,
                Message = "Usuário atualizado com sucesso",
                Data = new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                }
            };
        }

        public Result<UserViewModel> DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                return new Result<UserViewModel>()
                {
                    Success = false,
                    Message = "Usuário não encontrado",
                    Data = new UserViewModel()
                };
            }

            _dbContext.Remove(user);
            _dbContext.SaveChanges();

            return new Result<UserViewModel>()
            {
                Success = true,
                Message = "Usuário deletado com sucesso",
                Data = new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                }
            };
        }

    }
}
