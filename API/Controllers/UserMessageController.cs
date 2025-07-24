using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Controllers
{
    [Route("v1/api/user/messages")]
    public class UserMessageController : BaseController<UserMessageModel, IBaseController<UserMessageModel>>
    {
        public UserMessageController(IBaseController<UserMessageModel> service) : base(service) { }
    }
}