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
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _logIn.CheckLogin(loginDto.EmailOrCPF.ToUpper(), loginDto.Password);

            return StatusCode(result._StatusCode, result._Data == null ? new { Message = result._Message } : new { Id = result._Data });
        }
    }
}
