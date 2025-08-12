using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Controllers
{
    [Route("v1/api/bot")]
    public class BotController : BaseController<BotModel, IBaseController<BotModel>>
    {
        public BotController(IBaseController<BotModel> service) : base(service) { }
    }
}