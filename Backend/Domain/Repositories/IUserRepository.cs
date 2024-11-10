using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> CreateUser(User user);
        Task<User> GetUserById(Guid id);
        Task UpdateUser(User user);
        Task DeleteUser(Guid id);
        Task<Guid> LoginUser(User user);
    }
}
