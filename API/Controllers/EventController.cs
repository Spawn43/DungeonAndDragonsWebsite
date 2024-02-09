using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
                _eventService = eventService;
        }


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {

            Event ev = _eventService.GetEvent(id);
            if(ev == null)
            {
                return BadRequest();
            }
            string output = JsonConvert.SerializeObject(ev, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Ok(output);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] CreateEvent ev)
        {
            string eventId = _eventService.CreateEvent(ev);
            if(eventId == "error") {
                return StatusCode(500);
            }
            else
            {
                return StatusCode(200, new { EventId = eventId});
            }
        }
    }
}
