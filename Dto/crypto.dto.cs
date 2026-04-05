namespace cryptoapi.Dto
{
    public class CryptoResponseDto
    {
        public string cryptoCode { get; set; }
        public decimal amount { get; set; }
    }

    public class CreateCryptoDto
    {
        public string cryptoCode { get; set; }
        public decimal amount { get; set; }
    }

    public class CryptoPriceDto
    {
        public decimal ask { get; set; }
        public decimal bid { get; set; }
        public decimal totalAsk { get; set; }
        public decimal totalBid { get; set; }
        public long time { get; set; }
    }
}
