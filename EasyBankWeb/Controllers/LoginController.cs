using EasyBankWeb.Dto;
using EasyBankWeb.Repository;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    public class LoginController
    {
        private readonly LogIn _logIn;
        
        public LoginController(LogIn logIn)
        {
            _logIn = logIn;
        }

        //[Route("GetUsers")]
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    var result = _logIn.CheckLogin();
        //    return Ok(_register.GetUsers());
        //}

        //[Route("RegisterUser")]
        //[HttpPost]
        //public IActionResult Register([FromBody] UserDto userDto)
        //{
        //    var result = _register.UserRegisterSucessed(userDto);
        //    return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        //}

        //[Route("DeleteUser")]
        //[HttpDelete]
        //public IActionResult DeleteAccount([FromBody] UserDto userDto, bool confirmed)
        //{
        //    var result = _cancelAccountService.DeleteProcess(userDto, confirmed);

        //    return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        //}
    }
}
