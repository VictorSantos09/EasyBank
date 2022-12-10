using EasyBankWeb.Services;

namespace EasyBankWeb.Controllers
{
    public class LoginController
    {
        private readonly LogIn _logIn;

        public LoginController(LogIn logIn)
        {
            _logIn = logIn;
        }

        //[Route("GetAll")]
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    var result = _logIn.CheckLogin();
        //    return Ok(_register.GetAll());
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
