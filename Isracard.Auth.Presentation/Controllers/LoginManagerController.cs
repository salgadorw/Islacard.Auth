
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Isracard.Auth.Application.Services;
using Isracard.Auth.Application.Services.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("/api/[Controller]")]
    public class LoginManagerController : ControllerBase
    { 

        private readonly IAuthService calculatorService;
        public LoginManagerController(IAuthService calculatorService)
        {

            this.calculatorService = calculatorService;
        }
        /// <summary>
        /// Clear the Login value
        /// </summary>
        /// <remarks>Remove the value stored on Login</remarks>
        /// <response code="200">OK</response>
        [HttpDelete("/{id}")] 
        [SwaggerOperation("/Delete")]
        public virtual async Task<IActionResult> ClearLogin(Guid id)
        {
            await calculatorService.DeleteLoginValue(id);
            return Ok();
        }

        /// <summary>
        /// Save the last result on Login
        /// </summary>
        /// <remarks>Store last operation result in Login</remarks>
        /// <response code="200">OK</response>
        [HttpPost]
        [SwaggerOperation("/Save")]
        public virtual async Task<IActionResult> SaveToLogin([FromBody] LoginDTO value)
        {            
            await calculatorService.AddLoginValue(value);
            return Ok();
        }

        /// <summary>
        /// Return the stored Logins 
        /// </summary>
        /// <response code="200">All Logins values</response>
        [HttpGet]
        [SwaggerOperation("/List")]
        [SwaggerResponse(statusCode: 200, type: typeof(LoginDTO), description: "Registered logins")]
        [Authorize]
        public virtual async Task<IActionResult> GetLogins()
        {
            return Ok(await calculatorService.GetLoginValues());
        }
    }
}
