using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Requests;
using ChatApi.Infrastructure.Services;

namespace ChatApi.Controllers
{
    [Route("v1/api/tenant")]
    public class TenantController : BaseController<TenantModel, IBaseController<TenantModel>>
    {
        protected new readonly TenantService _service;

        public TenantController(
            IBaseController<TenantModel> service,
            TenantService tenantService
        ) : base(service)
        {
            _service = tenantService;
        }

        /// <summary>
        /// Endpoint that retrieves a list of entities based on filters
        /// </summary>
        /// <param name="filter">Filter parameters</param>
        /// <returns>List of entities</returns>
        /// <response code="200">Returns the list of entities</response>
        [HttpGet]
        public virtual async Task<IActionResult> Read([FromQuery] PaginationRequest filter)
        {
            var result = await _service.Read(filter);
            return Ok(result);
        }
    }
}