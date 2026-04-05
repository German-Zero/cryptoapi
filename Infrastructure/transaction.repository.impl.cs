using cryptoapi.Domain;
using cryptoapi.Entity;
using Microsoft.EntityFrameworkCore;

namespace cryptoapi.Infrastructure
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetByUserIdAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.DateTime)
                .ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public Task UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
