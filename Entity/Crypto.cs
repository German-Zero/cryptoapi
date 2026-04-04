namespace cryptoapi.Entity
{
    public class Crypto
    {
        public int Id { get; set; }
        public string CryptoCode { get; set; }
        public decimal Amount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
