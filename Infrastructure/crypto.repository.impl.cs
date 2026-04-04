using cryptoapi.Domain;
using cryptoapi.Dto;
using cryptoapi.Entity;
using System.Data.Entity;

namespace cryptoapi.Infrastructure
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly AppDbContext _context;

        public CryptoRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<List<Crypto>> GetByUserIdAsync(int userId)
        {
            return await _context.Cryptos
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Crypto?> GetByIdAsync(int id)
        {
            return await _context.Cryptos.FindAsync(id);
        }

        public async Task AddAsync(Crypto crypto)
        {
            await _context.Cryptos.AddAsync(crypto);
        }

        public Task UpdateAsync(Crypto crypto)
        {
            _context.Cryptos.Update(crypto);
            return Task.CompletedTask;  
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
