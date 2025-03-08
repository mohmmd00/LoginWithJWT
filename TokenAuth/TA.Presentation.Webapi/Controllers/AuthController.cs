using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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



        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var response = _authService.Register(request);

            if (response.IsInformationCorrect)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        // POST: AuthController/Create
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

            var responce = _authService.Login(request);
            if (responce.IsInformationCorrect)
            {
                return Ok(responce);
            }
            else
            {
                return Unauthorized(responce);
            }

        }



        [Authorize]
        [HttpGet("SecretMessage")]
        public IActionResult GetSecretMessage() 
        {
            return Ok("This is my secret message : 1231231231232fjeoigneoerGVERGVBIe");
        }

    }
}
