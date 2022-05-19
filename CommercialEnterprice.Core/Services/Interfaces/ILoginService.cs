using CommercialEnterprise.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Core.Services.Interfaces
{
    public interface ILoginService
    {
        Task<User> AuthorizateAsync(string login, string password);
        Task RegisterAsync(User user);

        Task RestorePasswordAsync(string login, string keyWord, string password);
        Task<bool> IsUserExistsWithLoginAndKeyWord(string login, string keyWord);
    }
}
