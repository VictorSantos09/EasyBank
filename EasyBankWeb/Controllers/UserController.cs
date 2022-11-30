using EasyBankWeb.Dto;
using EasyBankWeb.Repository;
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

        [HttpGet(Name = "GetUser")]
        public IActionResult Get()
        {
            return Ok(_register.GetUsers());
        }

        [HttpPost(Name = "PostUser")]
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