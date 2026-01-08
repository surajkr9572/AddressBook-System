using AddressBook.Entity;
using AddressBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Interface
{
    interface IAddressBookFileIO
    {
        public  Task WriteToFileAsync(Dictionary<string, List<Contacts>> addressBooks);
       
        Dictionary<string, List<Contacts>> ReadFromFile();
       
    }
}
