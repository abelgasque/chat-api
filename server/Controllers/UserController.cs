using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Entities.Models;
using Server.Infrastructure.Entities.Interfaces;

namespace Server.Controllers
{
    [Route("v1/api/user")]
    public class UserController : BaseController<UserModel, IBaseController<UserModel>>
    {
        public UserController(IBaseController<UserModel> service) : base(service) { }
    }
}