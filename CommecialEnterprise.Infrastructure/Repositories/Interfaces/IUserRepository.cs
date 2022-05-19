using CommercialEnterprise.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<bool> FindByLoginAndPasswordAsync(string login, string password);
        Task<User> GetUserByLoginAndPasswordAsync(string login, string password);

        Task ChangePasswordByLoginAndKeyWordAsync(string login, string keyWord, string password);

        Task<bool> IsUserExistsWithLoginAndKeyWord(string login, string keyWord);
    }
}
