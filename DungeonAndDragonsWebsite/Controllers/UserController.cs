using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Repository;


namespace DungeonAndDragonsWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginTokenRepository _loginTokenRepository;
        public UserController(IUserRepository userRepository, ILoginTokenRepository loginTokenRepository)
        {
            _userRepository = userRepository;
            _loginTokenRepository = loginTokenRepository;
        }
        /* [HttpGet("User/{}")]
         [public async Task<IActionResult> Get()
         {

         }*/

        [HttpPost]
        public void PostRegister([FromBody] User user)
        {
            var returnCode = _userRepository.PostRegister(user);
            Console.WriteLine(returnCode);

        }

       
    }
}
