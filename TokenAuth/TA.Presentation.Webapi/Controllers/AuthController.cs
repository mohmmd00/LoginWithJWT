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
        private readonly SecretMessageService _secretMessageService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
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

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            _authService.Register(request);
            return Ok();
        }


        [Authorize]
        [HttpPost("createNewMessage")]
        public IActionResult CreateMessage(SecretMessageRequest request)
        {
            _secretMessageService.CreateNewMessage(request);
            return Ok("message created");
        }

        [Authorize]
        [HttpGet("AllMessages")]
        public IActionResult GetAllSecretMessages() 
        {
            //var listofmessages = _secretMessageService.GetSecretsAll();
            //return Ok(listofmessages);

            return Ok("this is my secret token");
        }

    }
}
