using Application.DTOs.Cart;
using Application.UseCases.Cart.Commands.AddToCart;
using Application.UseCases.Cart.Commands.RemoveFromCart;
using Application.UseCases.Cart.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator mediator;

        public CartController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCartById(Guid id)
        {
            return await mediator.Send(new GetCartByIdQuery(id));
        }


        [HttpPut("{id}/product/{productId}/add")]
        public async Task<ActionResult> AddToCart(Guid id, Guid productId)
        {
            await mediator.Send(new AddToCartCommand(id, productId));
            return NoContent();
        }

        [HttpPut("{id}/product/{productId}/remove")]
        public async Task<ActionResult> RemoveFromCart(Guid id, Guid productId)
        {
            await mediator.Send(new RemoveFromCartCommand(id, productId));
            return NoContent();
        }
    }
}
