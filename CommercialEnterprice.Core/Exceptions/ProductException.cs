using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Core.Exceptions
{
    public class ProductException : Exception
    {
        public ProductException(string message) : base(message)
        {
        }
    }
}
