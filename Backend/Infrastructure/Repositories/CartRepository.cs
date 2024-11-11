using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Exceptions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private ApplicationDbContext context;

        public CartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddToCart(Guid userId, Guid productId)
        {
            var cart = await context.Carts
             .Include(c => c.Products)
             .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new ResourceNotFoundException("Cart." + ExceptionsResource.NoResourceFound);
            }

            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new ResourceNotFoundException("Product." + ExceptionsResource.NoResourceFound);
            }

            cart.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(Guid userId, Guid productId)
        {
            var cart = await context.Carts
             .Include(c => c.Products)
             .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new ResourceNotFoundException("Cart." + ExceptionsResource.NoResourceFound);
            }

            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new ResourceNotFoundException("Product." + ExceptionsResource.NoResourceFound);
            }

            cart.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public Task<Cart> GetCartById(Guid id)
        {
            var cart = context.Carts.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            if (cart == null)
            {
                throw new ResourceNotFoundException(ExceptionsResource.NoResourceFound);
            }

            return Task.FromResult(cart);
        }
    }
}
