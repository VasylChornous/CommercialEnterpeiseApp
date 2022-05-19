using CommercialEnterprise.Infrastructure.Models;
using CommercialEnterprise.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EnterpriceContext context;

        public UserRepository(EnterpriceContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();
        }

        public async Task<bool> FindByLoginAndPasswordAsync(string login, string password)
        {
            var foundedUser = await context.Users.Where(x => x.Login == login && x.Password == password).Select(x => x).AsNoTracking().FirstOrDefaultAsync();
            return foundedUser == null ? false : true;
        }

        public async Task ChangePasswordByLoginAndKeyWordAsync(string login, string keyWord, string password)
        {
            var user = await context.Users.Where(x => x.Login == login && x.KeyWord == keyWord).Select(x => x).AsNoTracking().FirstOrDefaultAsync();
            user.Password = password;
            context.Users.Update(user);

            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password) =>
            await context.Users.Where(x => x.Login == login && x.Password == password).Select(x => x).AsNoTracking().FirstOrDefaultAsync();

        public async Task<bool> IsUserExistsWithLoginAndKeyWord(string login, string keyWord)
        {
            var user = await context.Users.Where(x => x.Login == login && x.KeyWord == keyWord).Select(x => x).AsNoTracking().FirstOrDefaultAsync();

            return user == null ? false : true;
        }
    }
}
