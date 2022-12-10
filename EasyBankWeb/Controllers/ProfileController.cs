using EasyBankWeb.Repository;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileConfig _profileConfig;

        public ProfileController(ProfileConfig profileConfig)
        {
            _profileConfig = profileConfig;
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
    }
}