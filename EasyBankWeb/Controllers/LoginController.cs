using EasyBankWeb.Dto;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly LogIn _logIn;

        public LoginController(LogIn logIn)
        {
            _logIn = logIn;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody]LoginDto loginDto)
        {
            var result = _logIn.CheckLogin(loginDto.EmailOrCPF.ToUpper(), loginDto.Password);

            if(result._StatusCode == 200)
            return StatusCode(result._StatusCode, result._Data == null ? new { Message = result._Message } : new { Successful = true, Id = result._Data });

            return StatusCode(result._StatusCode, new { Message = result._Message, Successful = false });
        }
    }
}
