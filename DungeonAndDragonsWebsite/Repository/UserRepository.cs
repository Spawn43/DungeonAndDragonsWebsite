using DungeonAndDragonsWebsite.Data;
using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace DungeonAndDragonsWebsite.Repository
{

    public interface IUserRepository
    {
        KeyValuePair<int, string> PostRegister(User user);
        KeyValuePair<int, User> PostLogin(Login login);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public KeyValuePair<int, User> PostLogin(Login login)
        {
            User emptyUser = new User();
            KeyValuePair<int, User> returnCode = new KeyValuePair<int, User>(409, emptyUser);
            User databaseUser = GetUser(login.Username);
            if (databaseUser != null)
            {
                if (CheckPassword(login.Password, databaseUser))
                {
                    returnCode = new KeyValuePair<int, User>(200, databaseUser);
                }
            }           
            return returnCode;
        }

        public KeyValuePair<int, string> PostRegister(User user)
        {
            KeyValuePair <int, string> returnCode = new KeyValuePair<int, string>(200,"");
            if (CheckUserAge(user.DateOfBirth))
            {
                returnCode = new KeyValuePair<int, string>(403, "Age Verification Failed");
            }
            else if (CheckIsEmailValid(user.Email))
            {
                returnCode = new KeyValuePair<int, string>(409, "Invalid Email");
            }
            else if (CheckUserExists(user.Username,user.Email))
            {
                returnCode = new KeyValuePair<int, string>(409, "Existing Username or Email");
            }            
            else
            {
                user = EncryptPassword(user);
                user.Id = user.Username + DateTime.Now.ToString() + new Random().Next(1000, 9999).ToString();
                PostAddToDb(user);
            }
                     
            return returnCode;
        }

        private User GetUser(string  username)
        {
            return _db.Users.FirstOrDefault(u => u.Username == username);
        }
        private bool CheckPassword(string password, User userDatabase)
        {
     
                string hashedPassword = HashPassword(password, userDatabase.PasswordSalt);
                return hashedPassword == userDatabase.PasswordHash;
            
          
        }

        static string HashPassword(string password, string salt)
        {
            // Combine the password and salt, then hash the result using a secure hash function (e.g., SHA-256)
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hashed bytes to a Base64-encoded string
                string hashedPassword = Convert.ToBase64String(hashedBytes);
                return hashedPassword;
            }
        }
        private bool PostAddToDb(User user)
        {
            var x = _db.Users.Add(user);
            _db.SaveChanges();

            return true;
        }

        private User EncryptPassword(User user)
        {
            byte[] saltBytes = new byte[32]; // Adjust the size of the salt based on your security requirements
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            user.PasswordSalt = Convert.ToBase64String(saltBytes);
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(user.PasswordHash + user.PasswordSalt);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hashed bytes to a Base64-encoded string
                user.PasswordHash = Convert.ToBase64String(hashedBytes);
                
            }

            return user;
        }

        static bool CheckIsEmailValid(string email)
        {
            // Define a regular expression for validating an Email
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            // Use Regex.IsMatch to check if the email matches the pattern
            return !Regex.IsMatch(email, emailPattern);
        }

        private bool CheckUserAge(DateTime dateOfBirth)
        {
            int age = (int) ((DateTime.Today - dateOfBirth).TotalDays/365) ;
            bool test = age < 18;
            return age<18;
        }

        private bool CheckUserExists(string username, string email) {

            List<User> dbUser = _db.Users
                    .Where(u => u.Username.Equals(username) || u.Email.Equals(email))
                    .ToList();

            return dbUser.Count>0;
        }
    }
}
