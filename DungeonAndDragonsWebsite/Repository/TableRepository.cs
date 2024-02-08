using DungeonAndDragonsWebsite.Data;
using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace DungeonAndDragonsWebsite.Repository
{
    public interface ITableRepository
    {
        public KeyValuePair<bool, Table> ClaimTable(TableClaim tc, User user);
        public Table UpdateTable(TableClaim tc, User user);
    }
    public class TableRepository : ITableRepository
    {

        private readonly ApplicationDbContext _db;
        public TableRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public KeyValuePair<bool, Table> ClaimTable(TableClaim tc, User user)
        {
            Table table = _db.Tables
                .Include(t => t.Event)
                .FirstOrDefault(t => t.Id == tc.TableId);
            if (table == null || table.DungeonMaster != null)
            {
                return new KeyValuePair<bool, Table>(false, table);
            }
            else
            {
                table.DungeonMaster = user;
                table.PlayersAllowed = tc.NoOfPlayer;
                _db.SaveChanges();
                return new KeyValuePair<bool, Table>(true, table);
            }
        }

        public Table UpdateTable(TableClaim tc, User user)
        {
            Table table = _db.Tables
             .Include(t => t.Event)
             .FirstOrDefault(t => t.Id == tc.TableId && (t.DungeonMaster == user || t.Event.Planner == user));
            if(table != null)
            {
                table.PlayersAllowed = tc.NoOfPlayer;
                _db.SaveChanges();
            }

            return table;
        }
    }
}
