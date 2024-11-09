using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICartRepository
    { 
        Task<Cart> GetCartById(Guid id);
        Task AddToCart(Guid id, Guid productId);
        Task RemoveFromCart(Guid id, Guid productId);
    }
}
