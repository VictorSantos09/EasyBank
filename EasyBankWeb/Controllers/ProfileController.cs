using EasyBankWeb.Entities;
using EasyBankWeb.Repository;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileRepository _profileRepository;

        public ProfileController(ProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpGet(Name = "GetProfile")]
        public IActionResult SeeInformation()
        {
            return Ok(_profileRepository.GetProfile());
        }

        [HttpPost(Name = "PostProfile")]
        public IActionResult ViewSecu([FromBody] Profile profile)
        {
            _profileRepository.AddProfile(profile);

            return Ok();
        }

    }
}