using DungeonAndDragonsWebsite.Data;
using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace DungeonAndDragonsWebsite.Repository
{
    public interface ITableRepository
    {

        public TableEntity GetTableById(string id);
        public bool UpdateDungeonMaster(UserEntity user, string tableId);
        public bool UpdatePlayersAllowed(UserEntity user, string tableId, int noOfPlayers);
        public bool SignUp(UserEntity user, string tableId);
    }
    public class TableRepository : ITableRepository
    {

        private readonly ApplicationDbContext _db;
        public TableRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public TableEntity GetTableById(string id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
            return (TableEntity)_db.Tables
            .Include(e => e.Players)
            .Include(e => e.DungeonMaster)
            .Include(e => e.Event)
            .Select(e => new TableEntity
            {
                Id = e.Id,
                PlayersAllowed = e.PlayersAllowed,
                DungeonMaster = new UserEntity { Id = e.DungeonMaster.Id, Username = e.DungeonMaster.Username },
                Players = e.Players.Select(p => new UserEntity { Id = p.Id, Username = p.Username }).ToList(),
                Event = new Event
                {
                    Id = e.Event.Id,
                    Name = e.Event.Name,
                    Location = e.Event.Location,
                    Date = e.Event.Date,
                    Planner = new UserEntity { Id = e.Event.Planner.Id, Username = e.Event.Planner.Username }
                } 
            })
            .FirstOrDefault(u => id == u.Id);
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        public bool UpdateDungeonMaster(UserEntity user, string tableId)
        {
            TableEntity table = _db.Tables
                .Include(t => t.Event)
                .Include(t => t.Players)
                .Include(t => t.DungeonMaster)
                .FirstOrDefault(t => t.Id == tableId);
            if (table == null)
            {
                return false;
            }
            else if (table.DungeonMaster == null)
            {
                table.DungeonMaster = user;
                _db.SaveChanges();
                return true;
            }
            if (table.DungeonMaster == user || table.Event.Planner == user)
            {
                table.DungeonMaster = null;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePlayersAllowed(UserEntity user, string tableId, int noOfPlayers)
        {
            TableEntity table = _db.Tables
             .Include(t => t.Event)
             .FirstOrDefault(t => t.Id == tableId && (t.DungeonMaster == user || t.Event.Planner == user));
            if (table != null)
            {
                table.PlayersAllowed = noOfPlayers;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

            
        }

        public bool SignUp(UserEntity user, string tableId)
        {
            TableEntity table = _db.Tables
                .Include(t => t.Event)
                .Include(t => t.Players)
                .FirstOrDefault(t => t.Id == tableId);
            if (table == null)
            {
                return false;
            }
            else if (table.PlayersAllowed > table.Players.Count)
            {
                table.Players.Add(user);
                _db.SaveChanges();
                return true;
            }
            else return false;
        }

       
       
    }
}
