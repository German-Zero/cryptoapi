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
}
