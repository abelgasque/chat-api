using Microsoft.AspNetCore.Mvc;
using SecurityApp.Web.Infrastructure.Entities.Models;
using SecurityApp.Web.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace SecurityApp.Web.Controllers
{
    [ApiController]
    //[Authorize(Roles = UserRoleConstant.CodeAdmin)]
    [Route("v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service) 
        {
            _service = service;
        }

        /// POST: customer
        /// <summary>
        /// Endpoint that inserts customer
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CustomerModel pEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            return new OkObjectResult(await _service.CreateAsync(pEntity));
        }


        /// GET: customer
        /// <summary>
        /// Endpoint that read customer by id
        /// </summary>
        [HttpGet("{pId}")]
        public async Task<ActionResult> ReadById(Guid pId)
        {
            return new OkObjectResult(await _service.ReadById(pId));
        }

        /// GET: customer
        /// <summary>
        /// Endpoint that read customer
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> Read()
        {
            return new OkObjectResult(await _service.Read());
        }

        /// PUT: customer
        /// <summary>
        /// Endpoint that updates customer
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] CustomerModel pEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            return new OkObjectResult(await _service.UpdateAsync(pEntity));
        }

        /// PUT: customer
        /// <summary>
        /// Endpoint that delete customer
        /// </summary>
        [HttpDelete("{pId}")]
        public async Task<ActionResult> DeleteAsync(Guid pId)
        {
            await _service.DeleteAsync(pId);
            return new OkObjectResult(null);
        }
    }
}
