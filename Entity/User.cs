namespace cryptoapi.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Crypto> Cryptos { get; set; }
    }
}
