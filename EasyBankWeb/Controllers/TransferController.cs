using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    public class TransferController : ControllerBase
    {
        private readonly Transfer _transfer;

        public TransferController(Transfer transfer)
        {
            _transfer = transfer;
        }

        [Route("TransferMoney")]
        [HttpPost]
        public IActionResult Transfer([FromBody] int userID, double value, string keyPix, bool confirmed)
        {
            var result = _transfer.TransferProcess(userID, keyPix, confirmed, value);

            return StatusCode(result._StatusCode, new { result._Message });
        }
    }
}
