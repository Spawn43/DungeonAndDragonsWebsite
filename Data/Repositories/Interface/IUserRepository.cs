using Data.Entities;

namespace Data.Repositories.Interface
{
    public interface IUserRepository
    {
        UserEntity GetUserByUsername(string username);
        UserEntity GetUserByEmail(string email);
        bool Insert(UserEntity user);
    }
}