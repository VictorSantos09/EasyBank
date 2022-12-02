using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
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

        [HttpGet(Name = "GetLoans")]
        public IActionResult Get()
        {
            return Ok(loan.GetLoan());
        }

        [HttpPost(Name = "PostLoan")]
        public IActionResult Post([FromBody] LoanDto loanDto)
        {
            var result = loan.LoanRequest(loanDto.OwnerID, loanDto, loanDto.Confirmed);

            return Ok(StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data));
        }
        
    }
}