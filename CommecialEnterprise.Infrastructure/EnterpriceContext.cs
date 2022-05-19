using CommercialEnterprise.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CommercialEnterprise.Infrastructure
{
    public class EnterpriceContext : DbContext
    {

        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;";

        protected EnterpriceContext(DbContextOptions options) : base(options)
        {
        }

        public EnterpriceContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BankCard> Cards { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
