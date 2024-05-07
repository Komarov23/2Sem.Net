using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public IResult Login([FromBody] User.LoginUser data) => _authService.Login(data.Email, data.Password);

        [HttpPost("register")]
        public IResult Register([FromBody] User.RegisterUser data) => _authService.Register(data);
    }
}
