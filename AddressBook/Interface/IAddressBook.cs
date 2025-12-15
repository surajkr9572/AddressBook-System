using AddressBook.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Interface
{
    interface IAddressBook
    {
        void AddContact(Contacts contact);
        List<Contacts> GetAllContacts();
    }
}
