using Inventory.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.Categories.Comands;
using Inventory.Application.CQRS.Queries;
using Inventory.Application.Categories.Queries;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly IQueryHandlerFactory _queryHandlerFactory;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICommandHandlerFactory commandHandlerFactory, 
            IQueryHandlerFactory queryHandlerFactory,
            ILogger<CategoryController> logger
            )
        {
            _commandHandlerFactory = commandHandlerFactory;
            _queryHandlerFactory = queryHandlerFactory;
            _logger = logger;
        }

        /// <summary>
        /// Get by id category
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpGet("{id}")]
        [ActionName("GetCategoryById")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            _logger.LogInformation("Get category by id");
            var handler = _queryHandlerFactory.CreateHandler<GetCategoryByIdQuery, CategoryDto>();
            var result = await handler.Handle(new GetCategoryByIdQuery { Id = id });

            return Ok(result);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpGet()]
        [ActionName("GetAll")]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            _logger.LogInformation("Get all categories");
            var handler = _queryHandlerFactory.CreateHandler<GetAllCategoriesQuery, List<CategoryDto>>();
            var result = await handler.Handle(new GetAllCategoriesQuery());

            return result;
        }

        /// <summary>
        /// Create category
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            _logger.LogInformation("Create category");
            var handler = _commandHandlerFactory.CreateHandler<CreateCategoryCommand, CategoryDto>();
            var result = await handler.Handle(createCategoryCommand);

            return Ok(result);
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpPut]
        public async Task<ActionResult<CategoryDto>> Update(UpdateCategoryCommand updateCategoryCommand)
        {
            _logger.LogInformation("Update category");
            var handler = _commandHandlerFactory.CreateHandler<UpdateCategoryCommand, CategoryDto>();
            var result = await handler.Handle(updateCategoryCommand);

            return Ok(result);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDto>> Delete(int id)
        {
            _logger.LogInformation("Delete category");
            var handler = _commandHandlerFactory.CreateHandler<DeleteCategoryCommand, CategoryDto>();
            var result = await handler.Handle(new DeleteCategoryCommand { Id = id });

            return Ok(result);
        }
    }
}
