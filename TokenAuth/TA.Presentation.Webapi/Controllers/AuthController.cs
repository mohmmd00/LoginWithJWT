using Microsoft.AspNetCore.Mvc;
using TA.Application.DTO_s;
using TA.Application.Services;

namespace TA.Presentation.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        // POST: AuthController/Create
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

            var responce = _authService.Login(request);
            return Ok(responce);

        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            _authService.Register(request);
            return Ok();
        }

    }
}
