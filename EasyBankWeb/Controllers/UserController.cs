using EasyBankWeb.Dto;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Register _register;
        private readonly CancelAccountService _cancelAccountService;

        public UserController(Register register, CancelAccountService cancelAccountService)
        {
            _register = register;
            _cancelAccountService = cancelAccountService;
        }

        [Route("GetUsers")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_register.GetUsers());
        }

        [Route("RegisterUser")]
        [HttpPost]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var result = _register.UserRegisterSucessed(userDto);
            return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        }

        [Route("DeleteUser")]
        [HttpDelete]
        public IActionResult DeleteAccount([FromBody] UserDto userDto, bool confirmed)
        {
            var result = _cancelAccountService.DeleteProcess(userDto, confirmed);

            return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        }
    }
}