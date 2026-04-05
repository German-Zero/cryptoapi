using cryptoapi.Entity;

namespace cryptoapi.Domain
{
    public interface ICryptoRepository
    {
        Task<Crypto?> GetByUserIdAndCode(int userId, string code);
        Task AddAsync(Crypto crypto);
        Task SaveChangesAsync();
    }
}
