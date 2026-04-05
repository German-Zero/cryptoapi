using cryptoapi.Application;
using cryptoapi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace cryptoapi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await _transactionService.GetByUser(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _transactionService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTransactionDto dto)
        {
            await _transactionService.Update(id, dto);
            return Ok("Transacion Actualizada.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _transactionService.Delete(id);
            return Ok("Transacion Eliminada.");
        }
    }
}
