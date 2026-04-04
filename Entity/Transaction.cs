namespace cryptoapi.Entity
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Money { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string CryptoCode { get; set; }
    }
}
