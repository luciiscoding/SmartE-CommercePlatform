using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Cart.Commands.AddToCart
{
    public class AddToCardCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private readonly ICartRepository cartRepository;

        public AddToCardCommandHandler(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            await cartRepository.AddToCart(request.UserId, request.ProductId);
        }
    }
}
