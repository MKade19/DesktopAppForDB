using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/auth")]
    public class AuthController : ControllerBase
    {
        private IAuthService AuthService { get; }

        public AuthController(IAuthService authService) 
        {
            AuthService = authService;
        }

        [HttpPost("login")]
        public async Task<AuthData> Login([FromBody] User user)
        {
            return await AuthService.Login(user);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task Logout()
        {
            await AuthService.Logout();
        }

        [HttpPost("register")]
        [Authorize(Roles = "admin")]
        public async Task Register([FromBody] User user)
        {
            await AuthService.Register(user);
        }
    }
}
