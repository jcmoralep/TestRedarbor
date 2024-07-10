using Inventory.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Inventory.Application.CQRS.Commands;
using Inventory.Application.InventoryManagement.Comands;
using Inventory.Application.CQRS.Queries;
using Inventory.Application.InventoryManagement.Queries;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InventoryController : ControllerBase
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly IQueryHandlerFactory _queryHandlerFactory;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ICommandHandlerFactory commandHandlerFactory, 
            IQueryHandlerFactory queryHandlerFactory, 
            ILogger<InventoryController> logger)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _queryHandlerFactory = queryHandlerFactory;
            _logger = logger;
        }

        /// <summary>
        /// Get by id inventory
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpGet("{id}")]
        [ActionName("GetInventoryById")]
        public async Task<ActionResult<InventoryDto>> Get(int id)
        {
            _logger.LogInformation("Get inventory by id");
            var handler = _queryHandlerFactory.CreateHandler<GetInventoryByIdQuery, InventoryDto>();
            var result = await handler.Handle(new GetInventoryByIdQuery { Id = id });

            return Ok(result);
        }

        /// <summary>
        /// Get all inventories
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpGet()]
        [ActionName("GetAll")]
        public async Task<ActionResult<List<InventoryDto>>> GetAll()
        {
            _logger.LogInformation("Get all inventories");
            var handler = _queryHandlerFactory.CreateHandler<GetAllInventoryQuery, List<InventoryDto>>();
            var result = await handler.Handle(new GetAllInventoryQuery());

            return result;
        }

        /// <summary>
        /// Create inventory
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult<InventoryDto>> Create([FromBody] CreateInventoryCommand createInventoryCommand)
        {
            _logger.LogInformation("Create inventory");
            var handler = _commandHandlerFactory.CreateHandler<CreateInventoryCommand, InventoryDto>();
            var result = await handler.Handle(createInventoryCommand);

            return Ok(result);
        }


        /// <summary>
        /// Update inventory
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpPut]
        public async Task<ActionResult<InventoryDto>> Update([FromBody] UpdateInventoryCommand updateInventoryCommand)
        {
            _logger.LogInformation("Update inventory");
            var handler = _commandHandlerFactory.CreateHandler<UpdateInventoryCommand, InventoryDto>();
            var result = await handler.Handle(updateInventoryCommand);

            return Ok(result);
        }

        /// <summary>
        /// Delete inventory
        /// </summary>        
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<InventoryDto>> Delete(int id)
        {
            _logger.LogInformation("Delete inventory");
            var handler = _commandHandlerFactory.CreateHandler<DeleteInventoryCommand, InventoryDto>();
            var result = await handler.Handle(new DeleteInventoryCommand { Id = id });

            return Ok(result);
        }
    }
}
