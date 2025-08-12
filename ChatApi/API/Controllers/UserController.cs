using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Infrastructure.Services;
using ChatApi.Domain.Requests;

namespace ChatApi.Controllers
{
    [Route("v1/api/user")]
    public class UserController : BaseController<UserModel, IBaseController<UserModel>>
    {
        protected new readonly UserService _service;

        public UserController(
            IBaseController<UserModel> service,
            UserService userService
        ) : base(service)
        {
            _service = userService;
        }

        /// <summary>
        /// Endpoint that retrieves a list of entities based on filters
        /// </summary>
        /// <param name="filter">Filter parameters</param>
        /// <returns>List of entities</returns>
        /// <response code="200">Returns the list of entities</response>
        [HttpGet]
        public virtual async Task<IActionResult> Read([FromQuery] UserFilterRequest filter)
        {
            var result = await _service.Read(filter);
            return Ok(result);
        }
    }
}