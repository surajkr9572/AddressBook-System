using AddressBook.Entity;
using AddressBook.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Service
{
    public class AddressBookService : IAddressBook
    {
        private List<Contacts> contactList = new List<Contacts>();
        public void AddContact(Contacts  contacts)
        {
            contactList.Add(contacts);
        }
        public List<Contacts> GetAllContacts()
        {
            return contactList;
        }
    }
}
