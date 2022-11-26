using EasyBankWeb.Dto;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SavingController : ControllerBase
    {
        private readonly Savings _savings;

        public SavingController(Savings savings)
        {
            _savings = savings;
        }

        [HttpGet(Name = "GetSaving")]
        public IActionResult Get()
        {
            return Ok(_savings.GetSavings());
        }

        [HttpPost(Name = "PostSaving")]
        public IActionResult Post([FromBody] SavingsDto savings)
        {
            _savings.AddSavings(savings);

            return Ok();
        }
        [HttpGet(Name = "Invest")]
        public IActionResult InvestMoney(int userID)
        {
            _savings.SavingSteps(userID);

            return Ok();
        }
    }
}