using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Entities.Interfaces;
using System;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v1/api/[controller]")]
    public abstract class BaseController<TModel, TService> : ControllerBase
        where TModel : class
        where TService : IBaseController<TModel>
    {
        protected readonly TService _service;

        protected BaseController(TService service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoint that inserts a new entity
        /// </summary>
        /// <param name="entity">Entity to be created</param>
        /// <returns>Created entity</returns>
        /// <response code="200">Returns the created entity</response>
        /// <response code="400">If the model is invalid</response>
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody] TModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreateAsync(entity);
            return Ok(entity);
        }

        /// <summary>
        /// Endpoint that gets an entity by its unique identifier
        /// </summary>
        /// <param name="id">Entity unique identifier</param>
        /// <returns>Entity with the given identifier</returns>
        /// <response code="200">Returns the entity</response>
        /// <response code="404">If the entity is not found</response>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> ReadById(Guid id)
        {
            var result = await _service.ReadById(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Endpoint that retrieves a list of entities based on filters
        /// </summary>
        /// <param name="filter">Filter parameters</param>
        /// <returns>List of entities</returns>
        /// <response code="200">Returns the list of entities</response>
        [HttpGet]
        public virtual async Task<IActionResult> Read([FromQuery] object filter)
        {
            var result = await _service.Read(filter);
            return Ok(result);
        }

        /// <summary>
        /// Endpoint that updates an existing entity
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>Updated entity</returns>
        /// <response code="200">Returns the updated entity</response>
        /// <response code="400">If the model is invalid</response>
        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync([FromBody] TModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(entity);
            return Ok(entity);
        }

        /// <summary>
        /// Endpoint that deletes an entity by its unique identifier
        /// </summary>
        /// <param name="id">Entity unique identifier</param>
        /// <returns>No content</returns>
        /// <response code="204">Entity deleted successfully</response>
        /// <response code="404">If the entity is not found</response>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _service.ReadById(id);
            if (entity == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}