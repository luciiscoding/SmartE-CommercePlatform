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

        [HttpGet]
        public async Task<ActionResult<CartDTO>> GetCartById()
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value!);

            return await mediator.Send(new GetCartByIdQuery(userId));
        }


        [HttpPut("add/{productId}")]
        public async Task<ActionResult> AddToCart(Guid productId)
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value!);

            await mediator.Send(new AddToCartCommand(userId, productId));
            return NoContent();
        }

        [HttpPut("remove/{productId}")]
        public async Task<ActionResult> RemoveFromCart(Guid productId)
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value!);

            await mediator.Send(new RemoveFromCartCommand(userId, productId));
            return NoContent();
        }
    }
}
