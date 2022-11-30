using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;
using EasyBankWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyBankWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly Register _register;

        public UserController(UserRepository userRepository, Register register)
        {
            _userRepository = userRepository;
            _register = register;
        }

        [HttpGet(Name = "GetUser")]
        public IActionResult Get()
        {
            return Ok(_userRepository.GetUsers());
        }

        [HttpPost(Name = "PostUser")]
        public IActionResult Post([FromBody] UserDto userDto)
        {
            _register.UserRegister(userDto);
        }
    }
}