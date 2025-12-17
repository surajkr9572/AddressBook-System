using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Exceptions
{
    public class PhoneInvalid:Exception
    {
        public PhoneInvalid(string msg) : base(msg) { }
    }
}
