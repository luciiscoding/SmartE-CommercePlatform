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


        [HttpPut("add/{productId}")]
        public async Task<ActionResult> AddToCart(Guid id, Guid productId)
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value!);

            await mediator.Send(new AddToCartCommand(userId, productId));
            return NoContent();
        }

        [HttpPut("remove/{productId}")]
        public async Task<ActionResult> RemoveFromCart(Guid id, Guid productId)
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value!);

            await mediator.Send(new RemoveFromCartCommand(userId, productId));
            return NoContent();
        }
    }
}
