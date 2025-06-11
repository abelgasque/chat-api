using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Entities.DTO;
using Server.Infrastructure.Services;
using System.Threading.Tasks;

namespace Server.Controllers
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
        /// Endpoint for User login 
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST v1/api/token
        ///     {
        ///         "username": "admin",
        ///         "password": "admin"
        ///     }
        ///
        /// </remarks>
        /// <param name="pEntity"></param>
        /// <returns>Returns User access token</returns>
        /// <response code="200">Returns access token of the request</response>
        /// <response code="404">Exception return if invalid password, email does not exist, User blocked / inactive or invalid template</response>
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
        /// Endpoint for User refresh login 
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
        /// <returns>Returns User access token</returns>
        /// <response code="200">Returns access token of the request</response>
        /// <response code="404">Exception return if invalid password, email does not exist, User blocked / inactive or invalid template</response>
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
