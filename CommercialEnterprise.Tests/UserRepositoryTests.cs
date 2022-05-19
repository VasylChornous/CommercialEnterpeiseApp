using CommercialEnterprise.Infrastructure;
using CommercialEnterprise.Infrastructure.Models;
using CommercialEnterprise.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private EnterpriceContext context;
        private UserRepository userRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<EnterpriceContext>()
                .Options;

            context = new FakeContext(options);
            userRepository = new UserRepository(context);

            context.Users.Add(new User
            {
                Name = "Test name",
                Surname = "Test surname",
                Login = "login",
                Password = "12345",
                KeyWord = "qwerty",
                Gender = "Male",
                Email = "email@gmail.com",
                TelephoneNumber = "+380666666666",
                Card = new BankCard
                {
                    Number = 1111222233334444,
                    Date = "11/12/2024",
                    CVV = 048
                }
            });

            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [Test]
        public async Task IsUserExistsWithLoginAndKeyWord_WhenUserExists_ReturnsTrue()
        {
            var actual = await userRepository.IsUserExistsWithLoginAndKeyWord("login", "qwerty");

            Assert.IsTrue(actual);
        }
        [Test]
        public async Task IsUserExistsWithLoginAndKeyWord_WhenUserNotExists_ReturnsFalse()
        {
            var actual = await userRepository.IsUserExistsWithLoginAndKeyWord("llllllll", "llllllll");

            Assert.IsFalse(actual);
        }
        [Test]
        public async Task GetUserByLoginAndPasswordAsync_WhenCalled_ReturnsUser()
        {
            var actual = await userRepository.GetUserByLoginAndPasswordAsync("login", "12345");

            Assert.IsNotNull(actual);
            Assert.AreEqual("login", actual.Login);
            Assert.AreEqual("12345", actual.Password);
        }

        [Test]
        public async Task AddAsync_WhenCalled_AddedUserInContext()
        {
            var user = new User
            {
                Name = "test",
                Surname = "test",
                Login = "test",
                Password = "test",
                KeyWord = "test",
                Gender = "Male",
                Email = "email@gmail.com",
                TelephoneNumber = "+380666666666",
                Card = new BankCard
                {
                    Number = 1111222233334444,
                    Date = "11/12/2024",
                    CVV = 048
                }
            };
            await userRepository.AddAsync(user);

            var actual = await context.Users.Where(x => x.Name == user.Name && x.Surname == user.Surname).Select(x => x).FirstOrDefaultAsync();

            Assert.IsNotNull(actual);
        }

        [Test]
        public async Task FindByLoginAndPasswordAsync_WhenUserExists_ReturnsTrue()
        {
            var actual = await userRepository.FindByLoginAndPasswordAsync("login", "12345");

            Assert.IsTrue(actual);
        }
        [Test]
        public async Task FindByLoginAndPasswordAsync_WhenUserNotExists_ReturnsFalse()
        {
            var actual = await userRepository.FindByLoginAndPasswordAsync("login54353", "12345678987978");

            Assert.IsFalse(actual);
        }

        [Test]
        public async Task ChangePasswordByLoginAndKeyWordAsync_WhenCalled_ChangedPassword()
        {
            await userRepository.ChangePasswordByLoginAndKeyWordAsync("login", "qwerty", "000");

            var actual = await context.Users.Where(x => x.Login == "login" && x.KeyWord == "qwerty").Select(x => x).FirstOrDefaultAsync();

            Assert.IsNotNull(actual);
            Assert.AreEqual("000", actual.Password);
        }
    }
}
