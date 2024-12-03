using Application.DTOs.Product;
using Application.UseCases.Product.Commands.CreateProduct;
using Application.UseCases.Product.Commands.DeleteProduct;
using Application.UseCases.Product.Commands.UpdateProduct;
using Application.UseCases.Product.Queries.GetAllProducts;
using Application.UseCases.Product.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Product.Controllers
{
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct(CreateProductDTO product)
        {
            var productId = await mediator.Send(new CreateProductCommand(product));
            return Created(nameof(ProductDTO), productId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(Guid id)
        {
            return await mediator.Send(new GetProductByIdQuery(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductDTO product)
        {
            await mediator.Send(new UpdateProductCommand(product));
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts(
         [FromQuery] string? type,
         [FromQuery] decimal? minPrice,
         [FromQuery] decimal? maxPrice,
         [FromQuery] int? minReview,
         [FromQuery] int pageNumber = 0,
         [FromQuery] int pageSize = 10)
        {
            var query = new GetAllProductsQuery(type, minPrice, maxPrice, minReview, pageNumber, pageSize);
            var response = await mediator.Send(query);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            await mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
