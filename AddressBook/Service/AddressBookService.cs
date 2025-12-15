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
        public void UpdateAddressBook(string firstName, string UpdatedlastName, string Updatedaddress, string Updatedcity, string Updatedstate, string Updatedzipcode, string Updatedemail)
        {
            for(int i=0;i<contactList.Count;i++)
            {
                if (contactList[i].FirstName == firstName)
                {
                    contactList[i].LastName = UpdatedlastName;
                    contactList[i].Address = Updatedaddress;
                    contactList[i].City = Updatedcity;
                    contactList[i].State = Updatedstate;
                    contactList[i].Email = Updatedemail;

                }
            }
            Console.WriteLine("Employee updated successfully.");
            return;
        }
    }
}
