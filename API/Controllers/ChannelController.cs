using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Controllers
{
    [Route("v1/api/channel")]
    public class ChannelController : BaseController<ChannelModel, IBaseController<ChannelModel>>
    {
        public ChannelController(IBaseController<ChannelModel> service) : base(service) { }
    }
}