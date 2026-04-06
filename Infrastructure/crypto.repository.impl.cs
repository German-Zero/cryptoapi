using cryptoapi.Domain;
using cryptoapi.Entity;
using Microsoft.EntityFrameworkCore;

namespace cryptoapi.Infrastructure
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly AppDbContext _context;

        public CryptoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Crypto?> GetByUserIdAndCode(int userId, string code)
        {
            return await _context.Cryptos
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CryptoCode == code);
        }

        public async Task<List<Crypto>> GetByUserIdAsync(int userId)
        {
            return await _context.Cryptos
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Crypto crypto)
        {
            await _context.Cryptos.AddAsync(crypto);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
