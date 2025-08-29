using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
using System.Threading.Tasks;
using ChatApi.Domain.Requests;
using ChatApi.Infrastructure.Services;

namespace ChatApi.Controllers
{
    [Route("v1/api/bot")]
    public class BotController : BaseController<BotModel, IBaseController<BotModel>>
    {
        protected new readonly BotService _service;

        public BotController(
            IBaseController<BotModel> service,
            BotService botService
        ) : base(service)
        {
            _service = botService;
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