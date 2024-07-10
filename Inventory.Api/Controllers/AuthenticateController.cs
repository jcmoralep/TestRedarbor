using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IIdentityService identityService, ILogger<AuthenticateController> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        /// <summary>
        /// Allows you to register
        /// </summary>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto request)
        {
            _logger.LogInformation("User registration attempt");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state");
                return BadRequest(new AuthFailedResponseDto
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage))
                });
            }
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);
            if (!authResponse.Success)
            {
                _logger.LogError("Invalid registration attempt");
                return BadRequest(new AuthFailedResponseDto
                {
                    Errors = authResponse.Errors
                });
            }
            _logger.LogInformation("User registered successfully");

            return Ok(new AuthSuccessResponseDto
            {
                Token = authResponse.Token
            });
        }

        /// <summary>
        /// Allows you to authenticate
        /// </summary>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
        {
            _logger.LogInformation("User login attempt");
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (!authResponse.Success)
            {
                _logger.LogError("Invalid login attempt");
                return BadRequest(new AuthFailedResponseDto
                {
                    Errors = authResponse.Errors
                });
            }
            _logger.LogInformation("User logged in successfully");

            return Ok(new AuthSuccessResponseDto
            {
                Token = authResponse.Token
            });
        }
    }
}
