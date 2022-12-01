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
            loan.AddLoan(loanDto);

            return Ok();
        }
        [Route("AplyLoan")]
        [HttpPost]
        public IActionResult ApplylLoan([FromBody] LoanDto loandto)
        {
            if (loandto.Open == true)
            {
                var result = loan.ApplyLoan(loandto.RemainParcels, loandto.Value, loandto.Id);
                return Ok(result);
            }
            return Ok("");
        } 
    }
}