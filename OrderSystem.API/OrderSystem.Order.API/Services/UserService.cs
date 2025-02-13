using OrderSystem.Order.API.Infrastructure.Database;
using OrderSystem.Order.API.Models;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Models.DTOs.User;
using OrderSystem.Order.API.Services.Interfaces;

namespace OrderSystem.Order.API.Services
{
    public class UserService : IUserService
    {
        private OrderSystemDbContext _dbContext;

        public UserService(OrderSystemDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public Result CreateUser(UserRequest user)
        {
            User createdUser = new User
            {
                Email = user.Email,
                Name = user.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            _dbContext.Users.Add(createdUser);
            _dbContext.SaveChanges();

            return new Result
            {
                Success = true,
                Message = "Usuário cadastrado com sucesso.",
                Data = new UserViewModel 
                {
                    Id = createdUser.Id,
                    Email = createdUser.Email,
                    Name = createdUser.Name
                }
            };
        }

        public Result RetrieveUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);

            if (user  == null) {
                return new Result
                {
                    Success = false,
                    Message = "Usuário não encontrado.",
                    Data = new UserViewModel()
                };
            }

            return new Result
            {
                Success = true,
                Message = "Usuário encontrado.",
                Data = new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                }
            };
        }

        public Result UpdateUser(UserRequest userUpdate, int id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                return new Result
                {
                    Success = false,
                    Message = "Usuário não encontrado",
                    Data = new UserViewModel()
                };
            }

            if(!string.IsNullOrWhiteSpace(userUpdate.Name))
                user.Name = userUpdate.Name;

            if(!string.IsNullOrWhiteSpace(userUpdate.Email))
                user.Email = userUpdate.Email;

            if (!string.IsNullOrEmpty(userUpdate.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(userUpdate.Password); 

            _dbContext.SaveChanges();

            return new Result
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

        public Result DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                return new Result
                {
                    Success = false,
                    Message = "Usuário não encontrado",
                    Data = new UserViewModel()
                };
            }

            _dbContext.Remove(user);
            _dbContext.SaveChanges();

            return new Result
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
