using CommercialEnterprise.Core.Services.Interfaces;
using CommercialEnterprise.Infrastructure.Models;
using CommercialEnterprise.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository userRepository;

        public LoginService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<User> AuthorizateAsync(string login, string password)
        {
            if (await userRepository.FindByLoginAndPasswordAsync(login, password))
            {
                return await userRepository.GetUserByLoginAndPasswordAsync(login, password);
            }

            return null;
        }


        public async Task RegisterAsync(User user) =>
            await userRepository.AddAsync(user);

        public async Task RestorePasswordAsync(string login, string keyWord, string password) =>
            await userRepository.ChangePasswordByLoginAndKeyWordAsync(login, keyWord, password);

        public async Task<bool> IsUserExistsWithLoginAndKeyWord(string login, string keyWord) =>
            await userRepository.IsUserExistsWithLoginAndKeyWord(login, keyWord);
    }
}
