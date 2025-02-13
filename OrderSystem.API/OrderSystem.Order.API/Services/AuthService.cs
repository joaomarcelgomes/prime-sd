using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OrderSystem.Order.API.Infrastructure.Database;
using OrderSystem.Order.API.Models;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Services.Interfaces;
using LoginRequest = OrderSystem.Order.API.Models.DTOs.Auth.LoginRequest;

namespace OrderSystem.Order.API.Services
{
    public class AuthService : IAuthService
    {
        private OrderSystemDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthService(OrderSystemDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        private string TokenGenerator(User user)
        {
            string secretKey = _configuration["JwtSettings:SecretKey"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"], 
                claims: claims, 
                expires: DateTime.Now.AddDays(30), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Result Login(LoginRequest login)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Email == login.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return new Result
                {
                    Success = false,
                    Message = "Credenciais inválidas.",
                    Data = ""
                };
            }

            var token = TokenGenerator(user);

            return new Result
            {
                Success = true,
                Message = "Autenticação bem-sucedida",
                Data = new {
                    Token = token,
                    UserId = user.Id
                }
            };
        }
    }
}
