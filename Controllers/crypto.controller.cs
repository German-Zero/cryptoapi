using cryptoapi.Application;
using cryptoapi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace cryptoapi.Controllers
{
    [ApiController]
    [Route("api/crypto")]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoService _cryptoService;

        public CryptoController(CryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallet(int userId)
        {
            try
            {
                var result = await _cryptoService.GetUserWallet(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCryptoToWallet(int userId, CreateCryptoDto dto)
        {
            try 
            {
                await _cryptoService.AddCryptoToWallet(userId, dto);
                return Ok("Crypto añadida a la wallet");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
