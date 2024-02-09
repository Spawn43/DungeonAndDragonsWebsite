using AutoMapper;
using Data.Entities;
using Data.Repositories.Interface;
using Domain.Models;
using System.Text;
using System.Security.Cryptography;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    public class LoginService : ILoginService
    {

        private readonly IUserRepository _userRepository;
        private readonly ILoginTokenService _loginTokenService;

        public LoginService(IUserRepository repository, ILoginTokenService loginTokenService)
        {
            _userRepository = repository;
            _loginTokenService = loginTokenService;
        }
        public KeyValuePair<int, string> Login(Login login)
        {


            UserEntity dbUser = _userRepository.GetUserByUsername(login.Username);
            if (dbUser == null)
            {
                return new KeyValuePair<int, string>(409, "Incorrect Username");
            }
            else if (CheckPassword(login.Password, dbUser))
            {
                string token = _loginTokenService.LoginTokenGeneration(dbUser, login.TTL);
                return new KeyValuePair<int, string>(200, token);
            }
            else
            {
                return new KeyValuePair<int, string>(409, "Incorrect Password");
            }


        }

        private bool CheckPassword(string password, UserEntity userDatabase)
        {

            string hashedPassword = HashPassword(password, userDatabase.PasswordSalt);
            return hashedPassword == userDatabase.PasswordHash;


        }

        private string HashPassword(string password, string salt)
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
    }
}
