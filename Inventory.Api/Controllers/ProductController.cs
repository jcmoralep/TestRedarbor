using Inventory.Application.CQRS.Commands;
using Inventory.Application.CQRS.Queries;
using Inventory.Application.DTOs;
using Inventory.Application.Products.Comands;
using Inventory.Application.Products.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly IQueryHandlerFactory _queryHandlerFactory;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ICommandHandlerFactory commandHandlerFactory, 
            IQueryHandlerFactory queryHandlerFactory, 
            ILogger<ProductController> logger)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _queryHandlerFactory = queryHandlerFactory;
            _logger = logger;
        }

        /// <summary>
        /// Get by id product
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpGet("{id}")]
        [ActionName("GetProductById")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            _logger.LogInformation("Get product by id");
            var handler = _queryHandlerFactory.CreateHandler<GetProductByIdQuery, ProductDto>();
            var result = await handler.Handle(new GetProductByIdQuery { Id = id });

            return Ok(result);
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpGet()]
        [ActionName("GetAll")]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            _logger.LogInformation("Get all products");
            var handler = _queryHandlerFactory.CreateHandler<GetAllProductQuery, List<ProductDto>>();
            var result = await handler.Handle(new GetAllProductQuery());

            return result.ToList();
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductCommand createProductCommand)
        {
            _logger.LogInformation("Create product");
            var handler = _commandHandlerFactory.CreateHandler<CreateProductCommand, ProductDto>();
            var result = await handler.Handle(createProductCommand);

            return Ok(result);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpPut]
        public async Task<ActionResult<ProductDto>> Update([FromBody] UpdateProductCommand updateProductCommand)
        {
            _logger.LogInformation("Update product");
            var handler = _commandHandlerFactory.CreateHandler<UpdateProductCommand, ProductDto>();
            var result = await handler.Handle(updateProductCommand);

            return Ok(result);
        }


        /// <summary>
        /// Delete product
        /// </summary>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            _logger.LogInformation("Delete product");
            var handler = _commandHandlerFactory.CreateHandler<DeleteProductCommand, ProductDto>();
            var result = await handler.Handle(new DeleteProductCommand { Id = id });

            return Ok(result);
        }
    }
}
