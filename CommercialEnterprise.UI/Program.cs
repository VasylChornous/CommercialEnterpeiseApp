using CommercialEnterprise.Core.Services;
using CommercialEnterprise.Core.Services.Interfaces;
using CommercialEnterprise.Infrastructure;
using CommercialEnterprise.Infrastructure.Models;
using CommercialEnterprise.Infrastructure.Repositories;
using CommercialEnterprise.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CommercialEnterprise.UI
{
    public class Program
    {
        private static EnterpriceContext context;

        private static IUserRepository userRepository;
        private static Infrastructure.Repositories.Interfaces.IProductRepository productRepository;

        private static ILoginService loginService;
        private static Core.Services.Interfaces.IProductService productService;

        private static AuthorizationPage authorizationPage;
        private static ProductPage productPage;

        private static User authUser;
        

        static void SetUp()
        {
            context = new EnterpriceContext();

            userRepository = new UserRepository(context);
            productRepository = new ProductRepository(context);

            loginService = new LoginService(userRepository);
            productService = new ProductService(productRepository);

            authorizationPage = new AuthorizationPage(loginService);
        }
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            SetUp();

            var authUser = await authorizationPage.ShowAuth();

            productPage = new ProductPage(productRepository, authUser);
            await productPage.ShowMain();


            //await userRepository.AddAsync(new User
            //{
            //    Name = "Test name",
            //    Surname = "Test surname",
            //    Login = "login",
            //    Password = "12345",
            //    KeyWord = "qwerty",
            //    Gender = "Male",
            //    Email = "email@gmail.com",
            //    TelephoneNumber = "+380677996848",
            //    Card = new BankCard
            //    {
            //        Number = 1111222233334444,
            //        Date = "11/12/2024",
            //        CVV = 048
            //    }
            //});
        }
        
    }
}
