using EasyBankWeb.Entities;
using EasyBankWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
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