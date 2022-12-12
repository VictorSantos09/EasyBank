using EasyBankWeb.Dto;
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
        [HttpPost]
        public IActionResult ViewBills([FromBody] BillDto billDto)
        {
            var result = _creditCard.ViewInvoice(billDto.UserID);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
        [Route("PayBills")]
        [HttpPost]
        public IActionResult ManuelPayment([FromBody] BillDto billDto)
        {
            var result = _creditCard.ManualMonthPaymentInvoice(billDto.UserID);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
    }
}
