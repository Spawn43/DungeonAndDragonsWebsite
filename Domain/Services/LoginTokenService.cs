using AutoMapper;
using Data.Entities;
using Data.Repositories.Interface;
using Domain.Models;
using Domain.Services.Interfaces;
using System.Text;


namespace Domain.Services
{
    public class LoginTokenService : ILoginTokenService
    {
        private static readonly Random RandomGenerator = new Random();
        private const string AlphanumericChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static ILoginTokenRepository _repository;
        public LoginTokenService(ILoginTokenRepository loginTokenRepository)
        {
            _repository = loginTokenRepository;
        }
        public string LoginTokenGeneration(UserEntity user, int ttl)
        {
            string token = GenerateRandomToken();
            LoginTokenEntity lt = new LoginTokenEntity();
            lt.Id = GenerateRandomId();
            lt.LoginDateTime = DateTime.Now;
            lt.User = user;
            lt.TTL = ttl;
            lt.Token = token;

            _repository.InsertLoginToken(lt);

            return token;


        }

        public UserEntity CheckToken(string token)
        {
            LoginTokenEntity lt = _repository.GetLoginTokenByToken(token);
            if (lt!=null)
            {
               
                return lt.User;
            }
            else
            {
                return null;
            }
            
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
    }
}
