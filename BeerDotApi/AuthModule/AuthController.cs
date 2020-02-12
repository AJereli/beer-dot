using System.Threading.Tasks;
using BeerDotApi.AuthModule.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeerDotApi.AuthModule
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenDto>> Login(LoginDto login)
        {
            _logger.LogInformation(2, "Login");
            var token = await _authService.Login(login.Email, login.Password);

            if (token == null)
            {
                return BadRequest();
            }

            return token;

        }
        
        
        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registration(RegistrationDto registrationDto)
        {
            _logger.LogInformation(1, "Registration");

            var result = await _authService.Registration(registrationDto);

            if (result)
            {
                return Ok();
            } 
            else
            {
                return BadRequest();
            }
            
        }
        
    }
}