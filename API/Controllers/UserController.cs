using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService userService)
        {
            _service = userService;
        }

        [HttpPost]
        public IActionResult PostRegister([FromBody] RegisterUser user)
        {

            KeyValuePair<int, string> returnCode = _service.RegisterUser(user);
            if(returnCode.Key == 200)
            {
                return Ok();
            }
            else
            {
                return StatusCode(returnCode.Key, new { Message = returnCode.Value });
            }
        }

       
    }
}
