using MediatR;

namespace Application.UseCases.Cart.Commands.AddToCart
{
    public class AddToCartCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        public AddToCartCommand(Guid userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}
