using cryptoapi.Domain;
using cryptoapi.Dto;
using cryptoapi.Entity;
using cryptoapi.Infrastructure;

namespace cryptoapi.Application
{
    public class WalletService
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public WalletService(
            ICryptoRepository cryptoRepository,
            ITransactionRepository transactionRepository,
            AppDbContext context,
            HttpClient httpClient)
        {
            _cryptoRepository = cryptoRepository;
            _transactionRepository = transactionRepository;
            _context = context;
            _httpClient = httpClient;
        }

        public async Task ExecuteTransaction(TransactionRequestDto dto)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            var Price = await GetPrice(dto.CryptoCode);

            var cryptoAmount = dto.AmountFiat / Price;

            var wallet = await _cryptoRepository
                .GetByUserIdAndCode(dto.UserId, dto.CryptoCode);

            if (dto.Action == "BUY")
            {
                if (wallet == null)
                {
                    wallet = new Crypto
                    {
                        UserId = dto.UserId,
                        CryptoCode = dto.CryptoCode,
                        Amount = 0
                    };

                    wallet.Amount += cryptoAmount;
                }
                else if (dto.Action == "SELL")
                {
                    if (wallet == null || wallet.Amount < cryptoAmount)
                    {
                        throw new Exception("Fondos Insuficientes");

                        wallet.Amount -= cryptoAmount;
                    }
                }

                await _transactionRepository.AddAsync(new Transaction
                {
                    UserId = dto.UserId,
                    CryptoCode = dto.CryptoCode,
                    Quantity = dto.AmountFiat,
                    Money = cryptoAmount,
                    Action = dto.Action,
                    DateTime = DateTime.UtcNow
                });

                await _cryptoRepository.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
        }
        private async Task<decimal> GetPrice(string cryptoCode)
        {
            var url = $"https://criptoya.com/api/satoshitango/{cryptoCode}/ARS/1";

            var response = await _httpClient.GetFromJsonAsync<CryptoPriceDto>(url);

            if (response == null)
                throw new Exception("Error al obtener precio");

            return response.ask;
        }
    }
}
