using MediatR;

namespace Application.UseCases.Cart.Commands.AddToCart
{
    public class AddToCartCommand : IRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        public AddToCartCommand(Guid cartId, Guid productId)
        {
            CartId = cartId;
            ProductId = productId;
        }
    }
}
