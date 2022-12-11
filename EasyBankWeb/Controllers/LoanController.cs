using EasyBankWeb.Dto;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly Loan loan;

        public LoanController(Loan _loan)
        {
            loan = _loan;
        }

        [HttpGet(Name = "GetAlls")]
        public IActionResult Get()
        {
            return Ok(loan.GetAll());
        }

        [HttpPost(Name = "PostLoan")]
        public IActionResult Post([FromBody] LoanDto loanDto)
        {
            var result = loan.LoanRequest(loanDto.OwnerID, loanDto, loanDto.Confirmed);

            return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        }

    }
}