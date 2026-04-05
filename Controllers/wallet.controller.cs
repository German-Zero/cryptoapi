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

        [HttpPost("transaction")]
        public async Task<IActionResult> Execute(TransactionRequestDto dto)
        {
            await _walletService.ExecuteTransaction(dto);
            return Ok("Transacion Completa.");
        }
    }
}
