using Application.DTOs.Cart;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Cart.Queries
{
    public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartDTO>
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public GetCartByIdQueryHandler(ICartRepository cartRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        public async Task<CartDTO> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetCartById(request.Id);
            return mapper.Map<CartDTO>(cart);
        }
    }
}
