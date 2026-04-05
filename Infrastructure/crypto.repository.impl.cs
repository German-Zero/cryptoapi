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

        public async Task<Crypto?> GetByUserIdAndCode(int userId, string code)
        {
            return await _context.Cryptos
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CryptoCode == code);
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
