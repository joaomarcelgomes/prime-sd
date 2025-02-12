﻿using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Order.API.Models.DTOs;
using OrderSystem.Order.API.Services.Interfaces;
using LoginRequest = OrderSystem.Order.API.Models.DTOs.Auth.LoginRequest;

namespace OrderSystem.Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<Result<string>> Login(LoginRequest login) 
        {
            var result = _authService.Login(login);

            return Ok(result);
        }
    }
}
