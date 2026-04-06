using cryptoapi.Entity;

namespace cryptoapi.Domain
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetByUserIdAsync(int userId);
        Task<Transaction> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
        Task SaveChangesAsync();
    }
}
