using Microsoft.AspNetCore.Mvc;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Controllers
{
    [Route("v1/api/tenant")]
    public class TenantController : BaseController<TenantModel, IBaseController<TenantModel>>
    {
        public TenantController(IBaseController<TenantModel> service) : base(service) { }
    }
}