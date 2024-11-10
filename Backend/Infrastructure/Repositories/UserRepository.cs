using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> CreateUser(User user)
        {
            await context.Users.AddAsync(user);
            await context.Carts.AddAsync(new Cart { User = user });
            await context.SaveChangesAsync();
            return user.Id;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public Task<User> GetUserById(Guid id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return Task.FromResult(user);
        }

        public async Task UpdateUser(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task<Guid> LoginUser(User user)
        {
            var userResult = context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (userResult == null)
            {
                throw new Exception("User not found");
            }
            return userResult.Id;
        }
    }
}
