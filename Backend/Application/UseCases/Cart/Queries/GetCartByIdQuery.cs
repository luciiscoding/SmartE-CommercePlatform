using Application.DTOs.Cart;
using MediatR;

namespace Application.UseCases.Cart.Queries
{
    public class GetCartByIdQuery : IRequest<CartDTO>
    {
        public Guid Id { get; set; }

        public GetCartByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
