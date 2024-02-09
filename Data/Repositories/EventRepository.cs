using Data.Context;
using Data.Entities;
using Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class EventRepository : IEventRepository
    {

        private readonly ApplicationDbContext _db;
        public EventRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public EventEntity GetById(string id){

            return _db.Events
            .Include(e => e.Planner)
            .Include(e => e.Tables)
            .ThenInclude(e => e.Players)
            .FirstOrDefault(u => id == u.Id);

        }

        public void PostEvent(EventEntity entity)
        {
            _db.Events.Add(entity);
            _db.SaveChanges();
        }
    }
}
