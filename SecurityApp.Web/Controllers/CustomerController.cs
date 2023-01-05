using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityApp.Web.Infrastructure.Entities.Filter;
using SecurityApp.Web.Infrastructure.Entities.Models;
using SecurityApp.Web.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace SecurityApp.Web.Controllers
{
    [ApiController, Authorize, Route("v1/api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        /// POST: v1/api/customer
        /// <summary>
        /// Endpoint that inserts customer
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST v1/api/customer
        ///     {
        ///         Id = null,
        ///         CreationDate = null,
        ///         UpdateDate = null,
        ///         FirstName = "Abel",
        ///         LastName = "Gasque L. Silva",
        ///         Mail = "contato.abelgasque@gmail.com",
        ///         Password = "admin",
        ///         AuthAttempts = 0,
        ///         Active = true,
        ///         Block = false,
        ///     }
        ///
        /// </remarks>
        /// <param name="pEntity"></param>
        /// <returns>Returns created entity model</returns>
        /// <response code="200">Returns customer of the request</response>
        /// <response code="404">Exception return if record does not exist, model invalid or email already registered</response>
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CustomerModel pEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            await _service.CreateAsync(pEntity);
            return new OkObjectResult(pEntity);
        }


        /// GET: v1/api/customer/{id}
        /// <summary>
        /// Endpoint that read customer by id
        /// </summary>
        /// <param name="pId"></param>
        /// <returns>Returns a model of the customer</returns>
        /// <response code="200">Returns customer of the request</response>
        /// <response code="404">Exception return if record does not exist</response>
        [HttpGet("{pId}")]
        public async Task<ActionResult> ReadById(Guid pId)
        {
            return new OkObjectResult(await _service.ReadById(pId));
        }

        /// GET: v1/api/customer
        /// <summary>
        /// Endpoint that read customer
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns>Returns lazy loading customer list with pagination</returns>
        /// <response code="200">Returns customer list of the request</response>
        [HttpGet]
        public async Task<ActionResult> Read([FromQuery] CustomerFilter pEntity)
        {
            return new OkObjectResult(await _service.Read(pEntity));
        }

        /// PUT: v1/api/customer
        /// <summary>
        /// Endpoint that updates customer
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT v1/api/customer
        ///     {
        ///         Id = "3fa85f64-5717-4562-b3fc-2c963f66afa6,
        ///         CreationDate = "2023-01-05T22:43:18.237Z",
        ///         UpdateDate = null,
        ///         FirstName = "Abel",
        ///         LastName = "Gasque L. Silva",
        ///         Mail = "contato.abelgasque@gmail.com",
        ///         Password = "admin",
        ///         AuthAttempts = 0,
        ///         Active = true,
        ///         Block = false,
        ///     }
        ///
        /// </remarks>
        /// <param name="pEntity"></param>
        /// <returns>Returns updated entity model</returns>
        /// <response code="200">Returns customer of the request</response>
        /// <response code="404">Exception return if record does not exist, model invalid or email already registered</response>
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] CustomerModel pEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            await _service.UpdateAsync(pEntity);
            return new OkObjectResult(pEntity);
        }

        /// DELETE: v1/api/customer
        /// <summary>
        /// Endpoint that delete customer
        /// </summary>
        /// <param name="pId"></param>
        /// <returns>Returns only the status or exception code on failure</returns>
        /// <response code="200">Returns only the status code of the request</response>
        /// <response code="404">Exception return if record does not exist</response>
        [HttpDelete("{pId}")]
        public async Task<ActionResult> DeleteAsync(Guid pId)
        {
            await _service.DeleteAsync(pId);
            return new OkObjectResult(null);
        }
    }
}
