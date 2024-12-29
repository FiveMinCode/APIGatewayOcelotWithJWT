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

        [HttpPost]
        [Route("Refreshtoken")]
        public IActionResult Refresh(Authentication tokenApiData) {
            string accessToken = tokenApiData.Token;
            string refreshToken = tokenApiData.RefreshToken;

            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(accessToken);
            var newAccessToken = _jwtTokenService.AuthToken(principal.Claims);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            // logic to update the token

            return Ok(new Authentication()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });

        }
    }
}
