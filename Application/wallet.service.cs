using cryptoapi.Domain;
using cryptoapi.Dto;
using cryptoapi.Entity;
using cryptoapi.Infrastructure;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<decimal> GetTotalInARS(int userId)
        {
            var wallet = await _cryptoRepository.GetByUserIdAsync(userId);

            decimal total = 0;

            foreach (var crypto in wallet)
            {
                var price = await GetPrice(crypto.CryptoCode);
                total += crypto.Amount * price.bid;
            }
            return total;
        }

        public async Task AddCryptoToUser(CreateCryptoDto dto)
        {
            var crypto = await _cryptoRepository
                .GetByUserIdAndCode(dto.userId, dto.cryptoCode);

            if (crypto == null)
            {
                crypto = new Crypto
                {
                    UserId = dto.userId,
                    CryptoCode = dto.cryptoCode,
                    Amount = dto.amount
                };
                
                await _cryptoRepository.AddAsync(crypto);
            }
            else
            {
                crypto.Amount += dto.amount;
            }
            await _cryptoRepository.SaveChangesAsync();
        }

        public async Task ExchangeCrypto(ExchangeCryptoDto dto)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            var fromWallet = await _cryptoRepository
                .GetByUserIdAndCode(dto.UserId, dto.FromCrypto);
            
            if (fromWallet == null || fromWallet.Amount < dto.Amount)
                throw new Exception("Fondos Insuficientes");

            var priceFrom = await GetPrice(dto.FromCrypto);
            var priceTo = await GetPrice(dto.ToCrypto);

            Console.WriteLine($"FROM BID: {priceFrom.bid}");
            Console.WriteLine($"TO ASK: {priceTo.ask}");

            var arsValue = dto.Amount * priceFrom.bid;
            var toAmount = arsValue / priceTo.ask;


            fromWallet.Amount -= dto.Amount;

            var toWallet = await _cryptoRepository
                .GetByUserIdAndCode(dto.UserId, dto.ToCrypto);

            if (toWallet == null)
            {
                toWallet = new Crypto
                {
                    UserId = dto.UserId,
                    CryptoCode = dto.ToCrypto,
                    Amount = 0
                };
                await _cryptoRepository.AddAsync(toWallet);
            }
            
            toWallet.Amount += toAmount;

            await _transactionRepository.AddAsync(new Transaction
            {
                UserId = dto.UserId,
                CryptoCode = $"{dto.FromCrypto} -> {dto.ToCrypto}",
                Quantity = dto.Amount,
                Money = arsValue,
                Action = "Exchange",
                DateTime = DateTime.UtcNow
            });

            await _cryptoRepository.SaveChangesAsync();
            await dbTransaction.CommitAsync();
        }
        private async Task<CryptoPriceDto> GetPrice(string cryptoCode)
        {
            var url = $"https://criptoya.com/api/satoshitango/{cryptoCode}/ARS";

            var response = await _httpClient.GetFromJsonAsync<CryptoPriceDto>(url);

            if (response == null)
                throw new Exception("Error al obtener precio");

            Console.WriteLine(response);
            return response;
        }
    }
}
