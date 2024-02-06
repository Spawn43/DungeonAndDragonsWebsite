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
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        [HttpPost("{username}")]
        public void PostLogin(string username, [FromBody] User user)
        {
            var returnCode = _userRepository.PostLogin(user);
            Console.WriteLine(returnCode);
        }
    }
}
