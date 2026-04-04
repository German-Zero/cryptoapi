using cryptoapi.Entity;

namespace cryptoapi.Domain
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task SaveChangeAsync();
    }
}
    