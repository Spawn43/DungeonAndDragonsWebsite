using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Repository;
using Newtonsoft.Json;

namespace DungeonAndDragonsWebsite.Services
{
    public interface ITableService
    {
        public TableEntity GetTable(string id);
    }
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly ILoginTokenRepository _loginTokenRepository;

        public TableService(ILoginTokenRepository loginTokenRepository, ITableRepository tableRepository)
        {
            _loginTokenRepository = loginTokenRepository;
            _tableRepository = tableRepository;
        }

        public TableEntity GetTable(string id)
        {
            TableEntity table = _tableRepository.GetTableById(id);
            return table;
        }
    }
}
