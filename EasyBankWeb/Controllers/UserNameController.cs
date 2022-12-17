using Microsoft.AspNetCore.Mvc;
using EasyBankWeb.Services;
using EasyBankWeb.Dto;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserNameController : ControllerBase
    {
        private readonly GetUserName GetName;

        public UserNameController(GetUserName getName)
        {
            GetName = getName;
        }

        [HttpPost]
        [Route("GetUserName")]
        public IActionResult GetUserName([FromBody] UserNameDto userName)
        {
            var result = GetName.SendUserName(userName.UserID);

            return StatusCode(result._StatusCode, result._Data == null ? result._Message 
                : new { Successful = true, Name = result._Data, MoneyAvaible = GetName.GetUserMoney(userName.UserID)._Data  });
        }
        [HttpPost]
        [Route("GetDateNow")]
        public IActionResult GetToday()
        {
            var today = DateTime.Today;

            var format = $"{today.Day}/{today.Month}/{today.Year}";

            return Ok(new { Today = format, Successful = true });
        }
    }
}
