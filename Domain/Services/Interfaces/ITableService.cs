using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITableService
    {
        void AddPlayer(LoginToken token, string tableId);
        public Table GetTable(string id);
        public bool ClaimTable(LoginToken token, string tableId);
        public bool SetPlayersAllowed(PlayersNoTable pnt, string tableId);
    }
}