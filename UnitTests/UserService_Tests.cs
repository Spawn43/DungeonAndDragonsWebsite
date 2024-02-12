using AutoMapper;
using Data.Repositories.Interface;
using NUnit.Framework.Constraints;
using Domain.Services.Interfaces;
using Domain.Services;
using Domain.Models;
using FakeItEasy;
using Data.Entities;


namespace UnitTests
{
    public class UserService_Tests
    {
        IUserService _userService;
        IUserRepository userRepository;

        [SetUp]
        public void Setup()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(Domain.Mappers.AutoMapper)));
            userRepository = A.Fake<IUserRepository>();
            A.CallTo(() => userRepository.Insert(A<UserEntity>._)).Returns(true);
            A.CallTo(() => userRepository.GetUserByEmail(A<string>._)).Returns(null);
            A.CallTo(() => userRepository.GetUserByUsername(A<string>._)).Returns(null);
            IMapper mapper = new Mapper(config);
            _userService = new UserService(userRepository, mapper);
        }

        [Test]
        public void RegisterUser_Test()
        {
            //ARRANGE
            RegisterUser ru = CreateRU("NewUser");
            //ACT

            KeyValuePair<int,string> response = _userService.RegisterUser(ru);

            // ASSERT
            Assert.AreEqual(200,response.Key);
        }
        [Test]
        public void RegisterUser_Age_Test()
        {
            //ARRANGE

            RegisterUser ru = CreateRU("NewUser");
            ru.DateOfBirth = DateTime.Now.AddMonths(-200);
            //ACT
            
            KeyValuePair<int, string> response = _userService.RegisterUser(ru);

            // ASSERT
            Assert.AreEqual("Age Verification Failed", response.Value);
        }
        [Test]
        public void RegisterUser_ExistingUsername_Test()
        {
            //ARRANGE
            UserEntity userEntity = new UserEntity();
            RegisterUser ru = CreateRU("NewUser");
            A.CallTo(() => userRepository.GetUserByUsername(A<string>._)).Returns(userEntity);
            //ACT

            KeyValuePair<int, string> response = _userService.RegisterUser(ru);

            // ASSERT
            Assert.AreEqual("Existing Username or Email", response.Value);
        }
        [Test]
        public void RegisterUser_ExistingEmail_Test()
        {
            //ARRANGE
            UserEntity userEntity = new UserEntity();
            RegisterUser ru = CreateRU("NewUser");
            A.CallTo(() => userRepository.GetUserByEmail(A<string>._)).Returns(userEntity);
            //ACT

            KeyValuePair<int, string> response = _userService.RegisterUser(ru);

            // ASSERT
            Assert.AreEqual("Existing Username or Email", response.Value);
        }
        [Test]
        public void RegisterUser_ValidEmail_Test()
        {
            //ARRANGE
            UserEntity userEntity = new UserEntity();
            RegisterUser ru = CreateRU("NewUser");

            ru.Email = "test";
            //ACT

            KeyValuePair<int, string> response = _userService.RegisterUser(ru);

            // ASSERT
            Assert.AreEqual("Invalid Email", response.Value);
        }
        [Test]
        public void RegisterUser_PasswordEncryption_Test()
        {
            //ARRANGE
            List<UserEntity> userEntities = new List<UserEntity>();
            UserEntity userEntity = new UserEntity();
            A.CallTo(() => userRepository.Insert(A<UserEntity>._))
                .Invokes((UserEntity data) => userEntities.Add(data));
            //ACT
            int i = 0;
            while (i< 10)
            {
                RegisterUser ru = CreateRU("NewUser"+i.ToString());
                ru.Password = "test";
                KeyValuePair<int, string> response = _userService.RegisterUser(ru);
                i++;
            }
            // ASSERT
            Assert.IsTrue(userEntities.Select(u => u.PasswordHash).Distinct().Count() == userEntities.Count,
                        "Password Hashes in the list are not unique.");
        }


        private RegisterUser CreateRU(string term)
        {
            RegisterUser ru = new RegisterUser();
            ru.Username = term+"Username";
            ru.Password = term+"Password";
            ru.FirstName = term + "FirstName";
            ru.Surname = term + "Surname";
            ru.Email = term + "@email.com";
            ru.DateOfBirth = DateTime.Now.AddMonths(-220);

            return ru;
        }
    }
}