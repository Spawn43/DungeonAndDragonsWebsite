using Data.Context;
using Data.Entities;
using Data.Repositories.Interface;


namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public UserEntity GetUserByUsername(string username)
        {
            UserEntity user = _db.Users
                    .FirstOrDefault(u => u.Username.Equals(username));
            return user;
        }

        public UserEntity GetUserByEmail(string email)
        {
            UserEntity user = _db.Users
                    .FirstOrDefault(u => u.Email.Equals(email));
            return user;
        }

        public bool Insert(UserEntity user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return true;
        }
    }
}
