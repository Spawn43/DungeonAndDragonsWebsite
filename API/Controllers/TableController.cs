﻿using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
                _tableService = tableService;
        }

        [HttpGet("{id}")]
        public IActionResult GetTable(string id)
        {
            Table table = _tableService.GetTable(id);
            if (table == null)
            {
                return BadRequest();
            }
            string output = JsonConvert.SerializeObject(table, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Ok(output);
        }

        [HttpPut("{id}/DungeonMaster")]
        public IActionResult ClaimTable(string id, [FromBody] LoginToken lt)
        {
            if (_tableService.ClaimTable(lt, id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}/PlayersAllowed")]
        public IActionResult PlayersAllowed(string id, [FromBody] PlayersNoTable pnt)
        {
            if (_tableService.SetPlayersAllowed(pnt, id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}/Players")]
        public IActionResult AddPlayer(string id, [FromBody] LoginToken token)
        {
            _tableService.AddPlayer(token, id);
            return Ok();
        }
    }
}
