using cryptoapi.Dto;
using cryptoapi.Entity;

namespace cryptoapi.Domain
{
    public interface ICryptoRepository
    {
        Task<List<Crypto>> GetByUserIdAsync(int userId);
        Task<Crypto?> GetByIdAsync(int id);
        Task AddAsync(Crypto crypto);
        Task UpdateAsync(Crypto crypto);
        Task SaveChangesAsync();
    }
}
