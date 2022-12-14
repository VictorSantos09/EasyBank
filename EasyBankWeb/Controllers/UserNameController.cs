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

            return StatusCode(result._StatusCode, new { Successful = true, Name = result._Data });
        }
    }
}
