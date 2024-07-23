using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        public AccountsController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authentication([FromBody] AuthenticationRequest authRequest)
        {
            var authResponse = _jwtTokenHandler.GenerateJwtToken(authRequest);
            if (authResponse == null)
            {
                return Unauthorized("Not Authorized");
            }
            return authResponse;
        }
    }
}
