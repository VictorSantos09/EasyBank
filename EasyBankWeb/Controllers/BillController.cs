using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly CreditCard _creditCard;

        public BillController(CreditCard creditCard)
        {
            _creditCard = creditCard;
        }
        [Route("ViewBills")]
        [HttpGet]
        public IActionResult ViewBills(int userID)
        {
            var result = _creditCard.ViewInvoice(userID);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
        [Route("PayBills")]
        [HttpPost]
        public IActionResult ManuelPayment([FromBody] int userID)
        {
            var result = _creditCard.ManualMonthPaymentInvoice(userID);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
    }
}
