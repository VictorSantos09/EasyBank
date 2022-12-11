using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    public class BillController : ControllerBase
    {
        private readonly CreditCard _creditCard;

        public BillController(CreditCard creditCard)
        {
            _creditCard = creditCard;
        }
        [HttpGet]
        [Route("ViewBills")]
        public IActionResult ViewBills(int userID)
        {
            var result = _creditCard.ViewInvoice(userID);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
        [HttpPost]
        [Route("PayBills")]
        public IActionResult ManuelPayment([FromBody] int userID)
        {
            var result = _creditCard.ManualMonthPaymentInvoice(userID);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
    }
}
