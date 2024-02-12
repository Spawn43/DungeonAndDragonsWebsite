using Data.Context;
using Data.Entities;
using Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TableRepository : ITableRepository
    {

        private readonly ApplicationDbContext _db;
        public TableRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public TableEntity GetById(string id)
        {

            return _db.Tables
                .Include(t => t.Players)
                .Include(t => t.DungeonMaster)
                .Include(t => t.Event)
                .FirstOrDefault(u => id == u.Id);

        }

        public void PostTable(TableEntity entity)
        {
            _db.Tables.Add(entity);
            _db.SaveChanges();
        }

        public void UpdateDataBase()
        {
            _db.SaveChanges();
        }
    }
}
