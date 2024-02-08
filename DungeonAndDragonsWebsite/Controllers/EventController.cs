using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DungeonAndDragonsWebsite.Models.Requests;

namespace DungeonAndDragonsWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly ILoginTokenRepository _loginTokenRepository;
        private readonly IEventRepository _eventRepository;
        public EventController(ILoginTokenRepository loginTokenRepository, IEventRepository eventRepository)
        {
            _loginTokenRepository = loginTokenRepository;
            _eventRepository = eventRepository;
        }

        [HttpPost]
        public void PostNewEvent([FromBody] EventCreation ec)
        {
            LoginToken lt = _loginTokenRepository.IsLoggedIn(ec.LoginToken);
            if (lt != null)
            {

                User user = lt.User;
                Event ev = _eventRepository.CreateEvent(ec, user);
            }
            Console.WriteLine("");
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
  /*          JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                // You can also use ReferenceLoopHandling.Serialize if you want to include references
                // ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            };*/
            Event ev = _eventRepository.GetEvent(id);
            string output = JsonConvert.SerializeObject(ev, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            return Ok(output);
        } 
    }
}
