using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly CreditCard _creditCard;

        public CreditCardController(CreditCard creditCard)
        {
            _creditCard = creditCard;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_creditCard.GetAll());
        }
    }
}
