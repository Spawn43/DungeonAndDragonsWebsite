using Data.Entities;

namespace Data.Repositories.Interface
{
    public interface IEventRepository
    {
        void PostEvent(EventEntity entity);
        EventEntity GetById(string id);
    }
}