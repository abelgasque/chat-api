using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatApi.Infrastructure.Services;
using System.Threading.Tasks;
using ChatApi.Domain.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatApi.Controllers
{
    [ApiController, AllowAnonymous, Route("v1/api/token")]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _service;
        private readonly UserService _userService;

        public TokenController(TokenService service, UserService userService)
        {
            _service = service;
            _userService = userService;
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
        ///         "username": "admin@example.com",
        ///         "password": "admin"
        ///     }
        ///
        /// </remarks>
        /// <param name="pEntity"></param>
        /// <returns>Returns User access token</returns>
        /// <response code="200">Returns access token of the request</response>
        /// <response code="404">Exception return if invalid password, email does not exist, User blocked / inactive or invalid template</response>
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] TokenRequest pEntity)
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
        public async Task<ActionResult> RefreshAsync([FromBody] RefreshTokenRequest pEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            return new OkObjectResult(await _service.Refresh(pEntity));
        }

        /// POST: v1/api/token/user
        /// <summary>
        /// Endpoint for create User lead 
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns>Returns User access token</returns>
        /// <response code="200">Returns access token of the request</response>
        /// <response code="404">Exception return if invalid password, email does not exist, User blocked / inactive or invalid template</response>
        [HttpPost("user")]
        public async Task<ActionResult> CreateLeadAsync([FromBody] UserLeadRequest pEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            await _userService.CreateLeadAsync(pEntity);
            return new OkResult();
        }

    }
}
