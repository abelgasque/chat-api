using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Infrastructure.Services;
using ChatApi.Domain.Requests;
using System;

namespace ChatApi.Controllers
{
    [Route("v1/api/chat")]
    public class ChatController : BaseController<ChatModel, IBaseController<ChatModel>>
    {
        protected new readonly ChatService _service;

        public ChatController(
            IBaseController<ChatModel> service,
            ChatService ChatService
        ) : base(service)
        {
            _service = ChatService;
        }

        /// <summary>
        /// Endpoint that retrieves a list of entities based on filters
        /// </summary>
        /// <param name="filter">Filter parameters</param>
        /// <returns>List of entities</returns>
        /// <response code="200">Returns the list of entities</response>
        [HttpGet]
        public virtual async Task<IActionResult> Read([FromQuery] ChatFilterRequest filter)
        {
            var result = await _service.Read(filter);
            return Ok(result);
        }
    }
}