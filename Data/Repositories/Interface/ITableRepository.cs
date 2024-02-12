using Data.Entities;

namespace Data.Repositories.Interface
{
    public interface ITableRepository
    {
        TableEntity GetById(string id);
        void PostTable(TableEntity entity);
        void UpdateDataBase();
    }
}