using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await _authenticationService.RegisterAsync(request);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authenticationService.LoginAsync(request);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}