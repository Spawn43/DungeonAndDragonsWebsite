using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ILoginService
    {
        KeyValuePair<int, string> Login(Login login);
    }
}