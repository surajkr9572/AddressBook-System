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
        void UpdateAddressBook(string FirstName, string uLastName, string uAddress, string uCity, string uState, string uZipcode, string uEmail);
        void DeletePerson(string name);
        bool SelectAddressBook(string bookName);
        bool CreateAddressBook(string bookName);
        List<string> GetAllAddressBookNames();
    }
}
