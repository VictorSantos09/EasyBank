using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class AutoDebitController : ControllerBase
    {
        private readonly AutoDebit _autoDebit;

        public AutoDebitController(AutoDebit autoDebit)
        {
            _autoDebit = autoDebit;
        }
        [Route("RegisterAutoDebit")]
        [HttpPost]
        public IActionResult Register([FromBody] AutoDebitDto autoDebitDto, bool confirmed, int userID)
        {
            var result = _autoDebit.RegisterAutoDebitProcess(confirmed, userID, autoDebitDto.Value);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
        [HttpGet]
        [Route("SeeAutoDebits")]
        public IActionResult SeeAutoDebits([FromBody] int userID)
        {
            var result = _autoDebit.DisplaysDebits(userID);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
        [HttpDelete]
        [Route("RemoveAutoDebit")]
        public IActionResult RemoveAutoDebit([FromBody] AutoDebitEntity autoDebitEntity)
        {
            var result = _autoDebit.Remove(autoDebitEntity);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
    }
}