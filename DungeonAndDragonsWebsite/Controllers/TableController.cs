using DungeonAndDragonsWebsite.Models.Requests;
using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DungeonAndDragonsWebsite.Controllers
{
    [Route("api/Table")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly ILoginTokenRepository _loginTokenRepository;
        private readonly ITableRepository _tableRepository;
        public TableController(ILoginTokenRepository loginTokenRepository, ITableRepository tableRepository)
        {
            _loginTokenRepository = loginTokenRepository;
            _tableRepository = tableRepository;
        }

        [HttpPost]
        public void PostClaimTable([FromBody] TableClaim tc)
        {
            LoginToken lt = _loginTokenRepository.IsLoggedIn(tc.LoginToken);
            if (lt != null)
            {

                User user = lt.User;
                KeyValuePair<bool, Table> returnCode = _tableRepository.ClaimTable(tc, user);

            }
            Console.WriteLine("");
        }

        [HttpPost("{id}")]
        public void UpdateTable([FromBody] TableClaim tc) {
            LoginToken lt = _loginTokenRepository.IsLoggedIn(tc.LoginToken);
            if (lt != null)
            {

                User user = lt.User;
                Table returnCode = _tableRepository.UpdateTable(tc, user);

            }
            Console.WriteLine("");
        }
    }
}
