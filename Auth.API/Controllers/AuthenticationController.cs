using Auth.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly JWTTokenService _jwtTokenService;

        public AuthenticationController(JWTTokenService jWTTokenService)
        {
            _jwtTokenService = jWTTokenService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login user)
        {
            var loginResult = _jwtTokenService.GetAuthToken(user);
            return loginResult is null ? Unauthorized(): Ok(loginResult);
        }
    }
}
