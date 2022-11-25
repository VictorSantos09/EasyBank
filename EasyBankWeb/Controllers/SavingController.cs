using EasyBankWeb.Entities;
using EasyBankWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SavingController : ControllerBase
    {
        private readonly SavingRepository _userRepository;

        public SavingController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(Name = "GetUser")]
        public IActionResult Get()
        {
            return Ok(_userRepository.GetUsers());
        }

        [HttpPost(Name = "PostUser")]
        public IActionResult Post([FromBody] User user)
        {
            _userRepository.AddUser(user);

            return Ok();
        }
    }
}