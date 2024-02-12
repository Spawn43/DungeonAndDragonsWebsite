using Data.Entities;

namespace Data.Repositories.Interface
{
    public interface IUserRepository
    {
        UserEntity GetUserByUsername(string username);
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserById(string id);
        bool Insert(UserEntity user);
    }
}