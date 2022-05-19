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
    public class ProductRepositoryTests
    {
        private EnterpriceContext context;
        private UserRepository userRepository;
        
        
        private ProductRepository productRepository;

        private User User { get; } = new User
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
        };
        private int userId;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<EnterpriceContext>()
                .Options;

            context = new FakeContext(options);
            productRepository = new ProductRepository(context);
            userRepository = new UserRepository(context);

            context.Users.Add(User);
            context.SaveChanges();

            userId = context.Users.Where(x => x.Name == User.Name && x.Surname == User.Surname).Select(x => x.Id).FirstOrDefault();

            context.Products.Add(new Product
            {
                Name = "product name",
                Quantity = 5,
                Price = 10,
                Description = "my desc",
                UserId = userId
            }
            );

            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [Test]
        public async Task AddProductAsync_WhenCalled_AddedProductInContext()
        {
            var product = new Product
            {
                Name = "test",
                Quantity = 1,
                Price = 1,
                Description = "none",
                UserId = userId,
            };

            await productRepository.AddProductAsync(product);

            var actual = await context.Products.Where(x => x.Name == product.Name).Select(x => x).FirstOrDefaultAsync();

            Assert.IsNotNull(actual);
            Assert.AreEqual(product.Name, actual.Name);
        }

        [Test]
        public async Task IsProductExistsByIdAsync_WhenProductExists_ReturnsTrue()
        {
            var productId = await context.Products.Where(x => x.Name == "product name").Select(x => x.Id).FirstOrDefaultAsync();
            var actual = await productRepository.IsProductExistsByIdAsync(productId);

            Assert.IsTrue(actual);
        }

        [Test]
        public async Task IsProductExistsByIdAsync_WhenProductExists_ReturnsFalse()
        {
            var productId = await context.Products.Where(x => x.Name == "fake name").Select(x => x.Id).FirstOrDefaultAsync();
            var actual = await productRepository.IsProductExistsByIdAsync(productId);

            Assert.IsFalse(actual);
        }

        [Test]
        public async Task DeleteProductByIdAsync_WhenCalled_DeletesProductWithSetId()
        {
            var productId = await context.Products.Where(x => x.Name == "product name").Select(x => x.Id).FirstOrDefaultAsync();

            await productRepository.DeleteProductByIdAsync(productId);

            var actualProduct = await context.Products.Where(x => x.Name == "product name").Select(x => x).FirstOrDefaultAsync();

            Assert.IsNull(actualProduct);
        }
    }
}
