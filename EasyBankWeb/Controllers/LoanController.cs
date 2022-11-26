using EasyBankWeb.Entities;
using EasyBankWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly LoanRepository _loanRepository;

        public LoanController(LoanRepository laonRepository)
        {
            _loanRepository = laonRepository;
        }

        [HttpGet(Name = "GetLoans")]
        public IActionResult Get()
        {
            return Ok(_loanRepository.GetLoan());
        }

        [HttpPost(Name = "PostLoan")]
        public IActionResult Post([FromBody] Loan loan)
        {
            _loanRepository.AddLoan(loan);

            return Ok();
        }

    }
}