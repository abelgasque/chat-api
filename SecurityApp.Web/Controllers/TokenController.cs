using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityApp.Web.Infrastructure.Entities.DTO;
using SecurityApp.Web.Infrastructure.Services;
using System.Threading.Tasks;

namespace SecurityApp.Web.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("v1/api/token")]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _service;
        
        public TokenController(TokenService service) 
        { 
            _service = service;
        }

        /// POST: v1/api/token/login
        /// <summary>
        /// Endpoint for customer login 
        /// </summary>
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
