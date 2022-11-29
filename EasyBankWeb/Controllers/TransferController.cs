using EasyBankWeb.Repository;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
            // Precisa mostrar o saldo em conta no front

            var result = _transfer.TransferProcess(userID, keyPix, confirmed, value);

            return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        }
    }
}
