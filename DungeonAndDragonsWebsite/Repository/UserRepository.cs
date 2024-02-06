using DungeonAndDragonsWebsite.Data;
using DungeonAndDragonsWebsite.Models;
using Microsoft.EntityFrameworkCore;


namespace DungeonAndDragonsWebsite.Repository
{

    public interface IUserRepository
    {
        KeyValuePair<int, String> PostUser(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public KeyValuePair<int, String> PostUser(User user)
        {
            KeyValuePair <int, String> ReturnCode = new KeyValuePair<int, String>(200,"");
            if (PostUserExists(user))
            {
                ReturnCode = new KeyValuePair<int, String>(409, "Existing Username or Email");
            }
            
            
            return ReturnCode;
        }

        private bool PostUserExists(User user) {

            List<User> dbUser = _db.Users
                    .Where(u => u.Username.Equals(user.Username) || u.Email.Equals(user.Email))
                    .ToList();

            return dbUser.Count>0;
        }
    }
}
