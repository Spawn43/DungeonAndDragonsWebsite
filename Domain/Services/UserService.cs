using Data.Repositories.Interface;
using Domain.Models;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Data.Entities;
using AutoMapper;
using Domain.Services.Interfaces;


namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// Public Methods

        public KeyValuePair<int, string> RegisterUser(RegisterUser registerUser)
        {
            KeyValuePair<int, string> returnCode = new KeyValuePair<int, string>(400, "");
            if (CheckUserAge(registerUser.DateOfBirth))
            {
                return new KeyValuePair<int, string>(403, "Age Verification Failed");
            }
            else if (CheckIsEmailValid(registerUser.Email))
            {
                return new KeyValuePair<int, string>(409, "Invalid Email");
            }
            else if (CheckUserExists(registerUser.Username, registerUser.Email))
            {
                return new KeyValuePair<int, string>(409, "Existing Username or Email");
            }
            else
            {

                UserEntity userEntity = _mapper.Map<UserEntity>(registerUser);
                userEntity.Id = userEntity.Username + DateTime.Now.ToString() + new Random().Next(1000, 9999).ToString();
                userEntity = EncryptPassword(userEntity, registerUser.Password);
                _repository.Insert(userEntity);
                return new KeyValuePair<int, string>(200, "User Added");

            };
        }

        //Private Methods

        private bool CheckUserAge(DateTime dateOfBirth)
        {
            int age = (int)((DateTime.Today - dateOfBirth).TotalDays / 365);
            bool test = age < 18;
            return age < 18;
        }

        private bool CheckIsEmailValid(string email)
        {
            // Define a regular expression for validating an Email
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            // Use Regex.IsMatch to check if the email matches the pattern
            return !Regex.IsMatch(email, emailPattern);
        }

        private bool CheckUserExists(string username, string email)
        {
            if (_repository.GetUserByUsername(username) == null || _repository.GetUserByEmail(email) == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private UserEntity EncryptPassword(UserEntity user, string password)
        {
            byte[] saltBytes = new byte[32]; // Adjust the size of the salt based on your security requirements
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            user.PasswordSalt = Convert.ToBase64String(saltBytes);
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + user.PasswordSalt);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hashed bytes to a Base64-encoded string
                user.PasswordHash = Convert.ToBase64String(hashedBytes);

            }

            return user;
        }

    }
}
