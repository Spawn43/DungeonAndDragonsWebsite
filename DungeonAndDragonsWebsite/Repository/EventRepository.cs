using DungeonAndDragonsWebsite.Data;
using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text;

namespace DungeonAndDragonsWebsite.Repository
{
    public interface IEventRepository
    {
        public Event CreateEvent(EventCreation ec, UserEntity user);
        public Event GetEvent(string id);
    }
    public class EventRepository : IEventRepository
    {
        private static readonly Random RandomGenerator = new Random();
        private const string AlphanumericChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly ApplicationDbContext _db;
        public EventRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Event GetEvent(string id) {

#pragma warning disable CS8603 // Possible null reference return.
            return _db.Events
            .Include(e => e.Planner)
            .Select(e => new Event
            {
                Id = e.Id,
                Name = e.Name,
                Location = e.Location,
                Date = e.Date,
                Planner = new UserEntity { Id = e.Planner.Id, Username = e.Planner.Username },
                Tables = e.Tables.Select(t => new TableEntity
                {
                    Id = t.Id,
                    Event = t.Event,
                    PlayersAllowed = t.PlayersAllowed,
                    DungeonMaster = new UserEntity { Id =t.DungeonMaster.Id, Username = t.DungeonMaster.Username},
                    Players = t.Players.Select(p => new UserEntity { Id = p.Id, Username = p.Username }).ToList()

                }).ToList()
            })
            .FirstOrDefault(u => id == u.Id);
#pragma warning restore CS8603 // Possible null reference return.

        }

        public Event CreateEvent(EventCreation ec, UserEntity user)
        {
            Event newEvent = new Event();
            newEvent.Id = GenerateRandomToken();
            newEvent.Planner = user;
            newEvent.Name = ec.EventName;
            newEvent.Location = ec.Location;
            newEvent.Planner = user;
            ICollection<TableEntity> tables = new List<TableEntity>();
            int i = 0;
            while (i < ec.NumberOfTables)
            {
                TableEntity t = new TableEntity();
                t.Id = GenerateRandomToken();
                t.Event = newEvent;
                tables.Add(t);
                i++;
            }
            newEvent.Tables = tables;
            AddEvent(newEvent);
            return newEvent;
        }

        private void AddEvent(Event ev)
        {
            _db.Events.Add(ev);
            _db.SaveChanges();
        }

        private string GenerateRandomToken()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 20; i++)
            {
                int index = RandomGenerator.Next(AlphanumericChars.Length);
                char randomChar = AlphanumericChars[index];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }
    }
}
