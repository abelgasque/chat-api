using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Infrastructure.Services;

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
    }
}