using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Services.Interfaces;

namespace API.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService loginService)
        {
             _service = loginService;
        }

        [HttpPost]
        public IActionResult PostLogin([FromBody] Login login)
        {
            KeyValuePair<int, string> returnCode = _service.Login(login);
            return StatusCode(returnCode.Key, returnCode.Value);

        }
    }
}
