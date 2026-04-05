using cryptoapi.Domain;
using cryptoapi.Dto;
using cryptoapi.Entity;

namespace cryptoapi.Application
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionResponseDto>> GetByUser(int userId)
        {
            var list = await _transactionRepository.GetByUserIdAsync(userId);

            return list.Select(t => new TransactionResponseDto
            {
                Id = t.Id,
                CryptoCode = t.CryptoCode,
                Quantity = t.Quantity,
                Money = t.Money,
                Action = t.Action,
                DateTime = t.DateTime,
            }).ToList();
        }

        public async Task<TransactionResponseDto> GetById(int id)
        {
            var t = await _transactionRepository.GetByIdAsync(id);

            if (t == null)
                throw new Exception("No Encontrada");

            return new TransactionResponseDto
            {
                Id = t.Id,
                CryptoCode = t.CryptoCode,
                Quantity = t.Quantity,
                Money = t.Money,
                Action = t.Action,
                DateTime = t.DateTime,
            };
        }

        public async Task Update(int id, UpdateTransactionDto dto)
        {
            var t = await _transactionRepository.GetByIdAsync(id);

            if (t == null)
                throw new Exception("No Encontrada");

            t.Quantity = dto.Quantity;
            t.Money = dto.Money;
            t.Action = dto.Action;
            t.CryptoCode = dto.CryptoCode;

            await _transactionRepository.UpdateAsync(t);
            await _transactionRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var t = await _transactionRepository.GetByIdAsync(id);

            if (t == null)
                throw new Exception("No Encontrada");

            await _transactionRepository.DeleteAsync(t);
            await _transactionRepository.SaveChangesAsync();
        }
    }
}
