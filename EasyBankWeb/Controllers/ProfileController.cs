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
        public IActionResult UpdateEmail([FromBody] string newemail, int userID)
        {
            var result = _profileConfig.ChangeEmail(newemail, userID);

            return StatusCode(result._StatusCode, new { result._Message });
        }

        [Route("ChangePassword")]
        [HttpPost]
        public IActionResult UpdatePassword([FromBody] string newPassword, int userID)
        {
            var result = _profileConfig.ChangePassword(newPassword, userID);

            return StatusCode(result._StatusCode, new { result._Message });
        }

        [Route("ChangePhoneNumber")]
        [HttpPost]
        public IActionResult UpdatePhoneNumber([FromBody] string newPhoneNumber, int userID)
        {
            var result = _profileConfig.ChangePhoneNumber(newPhoneNumber, userID);

            return StatusCode(result._StatusCode, new { result._Message });
        }
        [HttpGet]
        [Route("SeeProfile")]
        public IActionResult SeeProfile([FromBody] int userID, bool confirmed)
        {
            var result = _profile.ViewProfile(userID, confirmed);

            return StatusCode(result._StatusCode, result._Data == null ? new { result._Message } : result._Data);
        }
    }
}