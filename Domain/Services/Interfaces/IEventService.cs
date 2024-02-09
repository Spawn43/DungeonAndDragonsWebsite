using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IEventService
    {
        string CreateEvent(CreateEvent ev);
        Event GetEvent(string id);
    }
}