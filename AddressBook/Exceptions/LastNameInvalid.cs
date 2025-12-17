using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Exceptions
{
    public class LastNameInvalid:Exception
    {
        public LastNameInvalid(string msg):base(msg) { }
    }
}
