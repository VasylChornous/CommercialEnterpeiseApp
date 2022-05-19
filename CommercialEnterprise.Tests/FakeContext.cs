using CommercialEnterprise.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Tests
{
    public class FakeContext : EnterpriceContext
    {
        public FakeContext(DbContextOptions<EnterpriceContext> options) : base(options)
        {
        }
    }
}
