using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommandHandler: IRequestHandler<RemoveFromCartCommand>
    {
        private readonly ICartRepository cartRepository;

        public RemoveFromCartCommandHandler(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            await cartRepository.RemoveFromCart(request.CartId, request.ProductId);
        }
    }
}
