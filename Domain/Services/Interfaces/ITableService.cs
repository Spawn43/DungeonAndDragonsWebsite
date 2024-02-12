using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITableService
    {
        public KeyValuePair<int, string> AddPlayer(LoginToken token, string tableId);
        public Table GetTable(string id);
        public KeyValuePair<int, string> ClaimTable(LoginToken token, string tableId);
        public KeyValuePair<int, string> SetPlayersAllowed(PlayersNoTable pnt, string tableId);
        public KeyValuePair<int, string> RemovePlayer(LoginToken lt, string tableId, string userId);
    }
}