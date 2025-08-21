using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Infrastructure.Services;
using ChatApi.Domain.Requests;

namespace ChatApi.Controllers
{
    [Route("v1/api/user/transaction")]
    public class UserTransactionController : BaseController<UserTransactionModel, IBaseController<UserTransactionModel>>
    {
        protected new readonly UserTransactionService _service;

        public UserTransactionController(
            IBaseController<UserTransactionModel> service,
            UserTransactionService userTransactionService
        ) : base(service)
        {
            _service = userTransactionService;
        }

        /// <summary>
        /// Endpoint that retrieves a list of entities based on filters
        /// </summary>
        /// <param name="filter">Filter parameters</param>
        /// <returns>List of entities</returns>
        /// <response code="200">Returns the list of entities</response>
        [HttpGet]
        public virtual async Task<IActionResult> Read([FromQuery] PaginationRequest filter)
        {
            var result = await _service.Read(filter);
            return Ok(result);
        }
    }
}