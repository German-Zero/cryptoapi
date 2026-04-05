namespace cryptoapi.Dto
{
    public class TransactionRequestDto
    {
        public int UserId { get; set; }
        public string CryptoCode { get; set; }
        public decimal AmountFiat { get; set; }
        public string Action { get; set; }
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
    }
}
