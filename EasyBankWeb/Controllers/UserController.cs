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
        private readonly Profile _profile;

        public UserController(Register register, CancelAccountService cancelAccountService, Profile profile)
        {
            _register = register;
            _cancelAccountService = cancelAccountService;
            _profile = profile;
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_register.GetAll());
        }

        [Route("RegisterUser")]
        [HttpPost]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var result = _register.UserRegisterSucessed(userDto);
            return StatusCode(result._StatusCode, new { Message = result._Message });
        }

        [Route("DeleteUser")]
        [HttpDelete]
        public IActionResult DeleteAccount([FromBody] UserDto userDto, bool confirmed)
        {
            var result = _cancelAccountService.DeleteProcess(userDto, confirmed);

            return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        }

        [Route("SeeData")]
        [HttpGet]
        public IActionResult SeeInformation([FromBody] bool confirmed, int userID)
        {
            var result = _profile.ViewProfile(userID, confirmed);

            return StatusCode(result._StatusCode, result._Data == null ? result._Message : result._Data);
        }
    }
}