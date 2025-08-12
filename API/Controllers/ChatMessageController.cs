using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Infrastructure.Services;
using System.Threading.Tasks;
using ChatApi.Domain.Requests;

namespace ChatApi.Controllers
{
    [Route("v1/api/chat/message")]
    public class ChatMessageController : BaseController<ChatMessageModel, IBaseController<ChatMessageModel>>
    {
        protected new readonly ChatMessageService _service;

        public ChatMessageController(
            IBaseController<ChatMessageModel> service,
            ChatMessageService ChatMessageService
        ) : base(service)
        {
            _service = ChatMessageService;
        }

        /// <summary>
        /// Endpoint that retrieves a list of entities based on filters
        /// </summary>
        /// <param name="filter">Filter parameters</param>
        /// <returns>List of entities</returns>
        /// <response code="200">Returns the list of entities</response>
        [HttpGet]
        public virtual async Task<IActionResult> Read([FromQuery] ChatMessageFilterRequest filter)
        {
            var result = await _service.Read(filter);
            return Ok(result);
        }
    }
}