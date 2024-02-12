using AutoMapper;
using Data.Repositories.Interface;
using NUnit.Framework.Constraints;
using Domain.Services.Interfaces;
using Domain.Services;
using Domain.Models;
using FakeItEasy;
using Data.Entities;
using Azure;

namespace UnitTests
{
    public class LoginService_Tests
    {
        ILoginService _loginService;
        IUserRepository userRepository;
        ILoginTokenService loginTokenService;

        [SetUp]
        public void Setup()
        {
            UserEntity user = new UserEntity();
            user.Username = "test";
            user.PasswordSalt = "cOezWUywgaSEJ9sqkzy5EORt97rjqBkK04rOwTE2vKg="; //preexisting salt for password
            user.PasswordHash = "PqxfgEoRrYiWxbIpul9Q75m4vkS+Ms3BBUmRZdjUiTM="; //hash for password "string"
            userRepository = A.Fake<IUserRepository>();
            loginTokenService = A.Fake<LoginTokenService>();
            _loginService = new LoginService(userRepository, loginTokenService);
            A.CallTo(() => userRepository.GetUserByUsername(A<string>._)).Returns(null);
            A.CallTo(() => userRepository.GetUserByUsername("test")).Returns(user);
        }

        [Test]
        public void Login_test()
        {
            //ARRANGE
            Login login = new Login();
            login.Username = "test";
            login.Password = "string";

            //ACT
            KeyValuePair<int, string> response = _loginService.Login(login);

            //ASSERT
            Assert.AreEqual(200, response.Key);

        }

        [Test]
        public void Login_Username_test()
        {
            //ARRANGE
            Login login = new Login();
            login.Username = "incorrectUser";
            login.Password = "string";

            //ACT
            KeyValuePair<int, string> response = _loginService.Login(login);

            //ASSERT
            Assert.AreEqual("Incorrect Username", response.Value);

        }

        [Test]
        public void Login_Password_test()
        {
            //ARRANGE
            Login login = new Login();
            login.Username = "test";
            login.Password = "string1";

            //ACT
            KeyValuePair<int, string> response = _loginService.Login(login);

            //ASSERT
            Assert.AreEqual("Incorrect Password", response.Value);

        }



    }
}
