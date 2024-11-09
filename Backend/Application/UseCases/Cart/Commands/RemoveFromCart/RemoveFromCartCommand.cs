using MediatR;

namespace Application.UseCases.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommand : IRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        public RemoveFromCartCommand(Guid cartId, Guid productId)
        {
            CartId = cartId;
            ProductId = productId;
        }
    }
}
