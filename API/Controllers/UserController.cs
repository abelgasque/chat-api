using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Controllers
{
    [Route("v1/api/user")]
    public class UserController : BaseController<UserModel, IBaseController<UserModel>>
    {
        public UserController(IBaseController<UserModel> service) : base(service) { }
    }
}