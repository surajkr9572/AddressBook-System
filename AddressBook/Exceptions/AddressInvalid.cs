using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Exceptions
{
    public class AddressInvalid:Exception
    {
        public AddressInvalid(string msg):base(msg) { }
    }
}
