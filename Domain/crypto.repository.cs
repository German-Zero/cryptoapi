using cryptoapi.Entity;

namespace cryptoapi.Domain
{
    public interface ICryptoRepository
    {
        Task<Crypto?> GetByUserIdAndCode(int userId, string code);
        Task<List<Crypto?>> GetByUserIdAsync(int userId);
        Task AddAsync(Crypto crypto);
        Task SaveChangesAsync();
    }
}
