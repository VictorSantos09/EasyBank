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
        private readonly Profile _profile;
        private readonly CancelAccountService _cancelAccountService;

        public UserController(Register register, Profile profile, CancelAccountService cancelAccountService)
        {
            _register = register;
            _profile = profile;
            _cancelAccountService = cancelAccountService;
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

            return StatusCode(result._StatusCode, new { result._Message });
        }

        [Route("SeeData")]
        [HttpGet]
        public IActionResult SeeInformation([FromBody] bool confirmed, int userID)
        {
            var result = _profile.ViewProfile(userID, confirmed);

            return StatusCode(result._StatusCode, new { result._Message });
        }
    }
}