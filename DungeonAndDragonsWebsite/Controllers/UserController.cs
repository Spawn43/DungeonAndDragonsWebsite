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
        public void Post([FromBody] User user)
        {
            var returnCode = _userRepository.PostUser(user);
            Console.WriteLine(returnCode);

        }
    }
}
