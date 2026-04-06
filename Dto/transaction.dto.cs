namespace cryptoapi.Dto
{
    public class ExchangeCryptoDto
    {
        public int UserId { get; set; }
        public string FromCrypto { get; set; }
        public string ToCrypto { get; set; }
        public decimal Amount { get; set; }
    }

    public class TransactionResponseDto
    {
        public int Id { get; set; }
        public string CryptoCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Money { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class UpdateTransactionDto
    {
        public decimal Quantity { get; set; }
        public string Action { get; set; }
        public decimal Money { get; set; }
        public string CryptoCode { get; set; }
    }
}
