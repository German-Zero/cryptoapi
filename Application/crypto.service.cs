using cryptoapi.Domain;
using cryptoapi.Dto;
using cryptoapi.Entity;

namespace cryptoapi.Application
{
    public class CryptoService
    {
        private readonly ICryptoRepository _cryptoRepository;

        public CryptoService(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }

        public async Task<List<CryptoResponseDto>> GetUserWallet(int userId)
        {
            var cryptos = await _cryptoRepository.GetByUserIdAsync(userId);

            return cryptos.Select(c => new CryptoResponseDto
            {
                cryptoCode = c.CryptoCode,
                amount = c.Amount
            }).ToList();
        }

        public async Task AddCryptoToWallet(int userId, CreateCryptoDto dto)
        {
            var crypto = new Crypto
            {
                UserId = userId,
                CryptoCode = dto.cryptoCode,
                Amount = dto.amount
            };
            await _cryptoRepository.AddAsync(crypto);
            await _cryptoRepository.SaveChangesAsync();
        }
    }
}
