using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        KeyValuePair<int, string> RegisterUser(RegisterUser registerUser);
    }
}