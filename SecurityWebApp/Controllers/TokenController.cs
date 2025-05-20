using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityWebApp.Infrastructure.Entities.DTO;
using SecurityWebApp.Infrastructure.Services;
using System.Threading.Tasks;

namespace SecurityWebApp.Controllers
{
    [ApiController, AllowAnonymous, Route("v1/api/token")]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _service;
        
        public TokenController(TokenService service) 
        { 
            _service = service;
        }

        /// POST: v1/api/token
        /// <summary>
        /// Endpoint for customer login 
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST v1/api/token
        ///     {
        ///         "username": "contato.abelgasque@gmail.com",
        ///         "password": "admin"
        ///     }
        ///
        /// </remarks>
        /// <param name="pEntity"></param>
        /// <returns>Returns customer access token</returns>
        /// <response code="200">Returns access token of the request</response>
        /// <response code="404">Exception return if invalid password, email does not exist, customer blocked / inactive or invalid template</response>
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] UserDTO pEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }
            
            return new OkObjectResult(await _service.Login(pEntity));
        }

        /// POST: v1/api/token/refresh
        /// <summary>
        /// Endpoint for customer refresh login 
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST v1/api/token/refresh
        ///     {
        ///         "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
        ///         "id": "00000000-0000-0000-0000-000000000000"
        ///     }
        ///
        /// </remarks>
        /// <param name="pEntity"></param>
        /// <returns>Returns customer access token</returns>
        /// <response code="200">Returns access token of the request</response>
        /// <response code="404">Exception return if invalid password, email does not exist, customer blocked / inactive or invalid template</response>
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshAsync([FromBody] RefreshTokenDTO pEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            return new OkObjectResult(await _service.Refresh(pEntity));
        }

    }
}
