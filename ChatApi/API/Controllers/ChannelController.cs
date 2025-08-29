using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Requests;
using ChatApi.Infrastructure.Services;

namespace ChatApi.Controllers
{
    [Route("v1/api/channel")]
    public class ChannelController : BaseController<ChannelModel, IBaseController<ChannelModel>>
    {
        protected new readonly ChannelService _service;

        public ChannelController(
            IBaseController<ChannelModel> service,
            ChannelService channelService
        ) : base(service)
        {
            _service = channelService;
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