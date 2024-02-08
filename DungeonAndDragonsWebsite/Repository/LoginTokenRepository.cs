using DungeonAndDragonsWebsite.Data;
using DungeonAndDragonsWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DungeonAndDragonsWebsite.Repository
{
    public interface ILoginTokenRepository
    {
        public LoginToken IsLoggedIn(string loginToken);
        public string LoginTokenGeneration(User user, int ttl);
    }
    public class LoginTokenRepository : ILoginTokenRepository
    {
        private static readonly Random RandomGenerator = new Random();
        private const string AlphanumericChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly ApplicationDbContext _db;
        public LoginTokenRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public string LoginTokenGeneration(User user,int ttl)
        {
            string token = GenerateRandomToken();
            LoginToken lt = new LoginToken();
            lt.Id = GenerateRandomId();
            lt.LoginDateTime = DateTime.Now;
            lt.User = user;
            lt.TTL= ttl;
            lt.Token = token;

            _db.LoginTokens.Add(lt);
            _db.SaveChanges();

            return token;

        
        }

        public LoginToken IsLoggedIn (string loginToken)
        {
            return _db.LoginTokens
                .Include(u => u.User)
                .FirstOrDefault(u => loginToken == u.Token);
        }

        private string GenerateRandomToken()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 20; i++)
            {
                int index = RandomGenerator.Next(AlphanumericChars.Length);
                char randomChar = AlphanumericChars[index];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }
        private string GenerateRandomId()
        {
            long timestampPart = DateTime.UtcNow.Ticks;

            // Generate a random number for the additional randomness
            int randomPart = RandomGenerator.Next();

            // Combine timestamp and random parts to create a unique ID
            long randomId = (timestampPart << 32) | (uint)randomPart;

            // Convert the long to a string
            string randomIdString = randomId.ToString();

            return randomIdString;
        }
    
    }
}
