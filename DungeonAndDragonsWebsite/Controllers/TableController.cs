using DungeonAndDragonsWebsite.Models.Requests;
using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DungeonAndDragonsWebsite.Services;

namespace DungeonAndDragonsWebsite.Controllers
{
    [Route("api/Tables")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly ILoginTokenRepository _loginTokenRepository;
        private readonly ITableService _tableService;
        public TableController(ILoginTokenRepository loginTokenRepository, ITableService tableService)
        {
            _loginTokenRepository = loginTokenRepository;
            _tableService = tableService;
        }

        [HttpGet("{id}")]
        public IActionResult GetTable(string id)
        {
            TableEntity table = _tableService.GetTable(id);
            string output = JsonConvert.SerializeObject(table, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Ok(output);
        }


        [HttpPut("{id}/DungeonMaster")]
        public IActionResult UpdateDungeonMaster([FromBody] DefaultRequest dr, string id)
        {
            LoginToken lt = _loginTokenRepository.IsLoggedIn(dr.LoginToken);
            if (lt != null)
            {

                UserEntity user = lt.User;
                 bool returnCode = _tableRepository.UpdateDungeonMaster(user, id);

                if (returnCode)
                {
                    return Ok();
                }

            }
            return BadRequest();
        }

        [HttpPut("{id}/PlayersAllowed")]
        public IActionResult UpdatePlayersAllowed([FromBody] UpdatePlayersAllowedRequest upa, string id) {
            LoginToken lt = _loginTokenRepository.IsLoggedIn(upa.LoginToken);
            if (lt != null)
            {

                UserEntity user = lt.User;
                bool returnCode = _tableRepository.UpdatePlayersAllowed(user, id, upa.NoOfPlayer);
                if (returnCode)
                {
                    return Ok();
                }
                return BadRequest();

            }
            return BadRequest();
        }

        [HttpPut("{id}/players")]
        public IActionResult SignUp([FromBody] DefaultRequest dr, string id)
        {
            LoginToken lt = _loginTokenRepository.IsLoggedIn(dr.LoginToken);
            if (lt != null)
            {

                UserEntity user = lt.User;
                bool returnCode = _tableRepository.SignUp(user, id);
                if (returnCode)
                {
                    return Ok();
                }
                return BadRequest();

            }
            return BadRequest();
        }


    }
}
