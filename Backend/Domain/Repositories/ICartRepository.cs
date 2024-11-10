using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICartRepository
    { 
        Task<Cart> GetCartById(Guid id);
        Task AddToCart(Guid userId, Guid productId);
        Task RemoveFromCart(Guid userId, Guid productId);
    }
}
