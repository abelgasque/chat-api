using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityApp.Web.Infrastructure.Entities.DTO;
using SecurityApp.Web.Infrastructure.Services;
using System.Threading.Tasks;

namespace SecurityApp.Web.Controllers
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
        ///         Username = "contato.abelgasque@gmail.com",
        ///         Password = "admin",
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

    }
}
