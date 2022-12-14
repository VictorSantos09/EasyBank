using EasyBankWeb.Dto;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly Transfer _transfer;

        public TransferController(Transfer transfer)
        {
            _transfer = transfer;
        }

        [Route("TransferMoney")]
        [HttpPost]
        public IActionResult Transfer([FromBody] TransferMoneyDto moneyDto)
        {
            var result = _transfer.TransferProcess(moneyDto.UserID, moneyDto.KeyPix, moneyDto.Confirmed, moneyDto.Value);

            return StatusCode(result._StatusCode, new { result._Message });
        }
    }
}
