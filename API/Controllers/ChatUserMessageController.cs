using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Controllers
{
    [Route("v1/api/chat/message/user")]
    public class ChatUserMessageController : BaseController<ChatUserMessageModel, IBaseController<ChatUserMessageModel>>
    {
        public ChatUserMessageController(IBaseController<ChatUserMessageModel> service) : base(service) { }
    }
}