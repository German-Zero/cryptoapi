using cryptoapi.Application;
using cryptoapi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace cryptoapi.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly WalletService _walletService;

        public WalletController(WalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCrypto(CreateCryptoDto dto)
        {
            await _walletService.AddCryptoToUser(dto);
            return Ok("Cripto agregada");
        }

        [HttpGet("{userId}/total-ars")]
        public async Task<IActionResult> GetTotalInARS(int userId)
        {
            var total = await _walletService.GetTotalInARS(userId);
            return Ok(total);
        }

        [HttpPost("exchange")]
        public async Task<IActionResult> Execute(ExchangeCryptoDto dto)
        {
            await _walletService.ExchangeCrypto(dto);
            return Ok("Transacion Completa.");
        }
    }
}
