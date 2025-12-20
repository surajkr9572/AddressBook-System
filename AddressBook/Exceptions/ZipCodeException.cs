using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Exceptions
{
    public class ZipCodeException:Exception
    {
        public ZipCodeException(string msg):base(msg) { }
    }
}
