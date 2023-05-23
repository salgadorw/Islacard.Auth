using RWS.Authentication.Application.Services;
using RWS.Authentication.Application.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RWS.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {

        private readonly IAuthService authService;
        /// <summary>
        /// 
        /// </summary>       
        /// <param name="authService"></param>
        public AuthController( IAuthService authService)
        {
            
            this.authService = authService; 
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">Sign in user</param>
        /// <response code="200">Sign in user successfully</response>
        /// <response code="401">Bad credentials or user unauthorized</response>
        
        [HttpPost]              
        [SwaggerOperation("Login")]
        [SwaggerResponse(statusCode: 200, type: typeof(string), description: "JSON Web Token generated for user")]
        public virtual async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var tokenAsString = await authService.LogIn(login);

            if (!string.IsNullOrEmpty(tokenAsString)) 
                return Ok(tokenAsString);
            else
                throw new UnauthorizedAccessException(null, new InvalidDataException(null, new ApplicationException())); 
        }
    }
}
