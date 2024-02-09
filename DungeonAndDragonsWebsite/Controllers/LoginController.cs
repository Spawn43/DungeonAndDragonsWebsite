using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Models.Requests;
using DungeonAndDragonsWebsite.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DungeonAndDragonsWebsite.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginTokenRepository _loginTokenRepository;
        public LoginController(IUserRepository userRepository, ILoginTokenRepository loginTokenRepository)
        {
            _userRepository = userRepository;
            _loginTokenRepository = loginTokenRepository;
        }

        [HttpPost]
        public IActionResult PostLogin([FromBody] Login login)
        {
            KeyValuePair<int, UserEntity> returnCode = _userRepository.PostLogin(login);

            if (returnCode.Key == 200)
            {
                string token = _loginTokenRepository.LoginTokenGeneration(returnCode.Value, 200000);
                return Ok(token);
            }
            return BadRequest(returnCode);

        }
    }
}
