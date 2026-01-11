using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Exceptions
{
    public class FirstNameInvalid:Exception
    {
        public FirstNameInvalid(string msg):base(msg) { }
    }
}
