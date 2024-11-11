using Application.DTOs.Cart;
using MediatR;

namespace Application.UseCases.Cart.Queries
{
    public class GetCartByIdQuery : IRequest<CartDTO>
    {
        public Guid UserId { get; set; }

        public GetCartByIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
