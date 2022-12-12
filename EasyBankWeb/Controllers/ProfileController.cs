using EasyBankWeb.Dto;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileConfig _profileConfig;
        private readonly Profile _profile;

        public ProfileController(ProfileConfig profileConfig, Profile profile)
        {
            _profileConfig = profileConfig;
            _profile = profile;
        }

        [Route("ChangeEmail")]
        [HttpPost]
        public IActionResult UpdateEmail([FromBody] ChangeEmailDto emailDto)
        {
            var result = _profileConfig.ChangeEmail(emailDto.newEmail, emailDto.UserID);

            return StatusCode(result._StatusCode, new { result._Message });
        }

        [Route("ChangePassword")]
        [HttpPost]
        public IActionResult UpdatePassword([FromBody] ChangePasswordDto passwordDto)
        {
            var result = _profileConfig.ChangePassword(passwordDto.NewPassword, passwordDto.UserID);

            return StatusCode(result._StatusCode, new { result._Message });
        }

        [Route("ChangePhoneNumber")]
        [HttpPost]
        public IActionResult UpdatePhoneNumber([FromBody] ChangePhoneNumberDto phoneNumberDto)
        {
            var result = _profileConfig.ChangePhoneNumber(phoneNumberDto.NewPhoneNumber, phoneNumberDto.UserID);

            return StatusCode(result._StatusCode, new { result._Message });
        }
    }
}