using AddressBook.Entity;
using AddressBook.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook.Service
{
    public class AddressBookService : IAddressBook
    {
        private Dictionary<string, List<Contacts>> addressBooks = new Dictionary<string, List<Contacts>>();
        private string currentBookName = null;

        // Create a new address book and set it as current
        public bool CreateAddressBook(string bookName)
        {
            if (addressBooks.ContainsKey(bookName))
                return false;

            addressBooks[bookName] = new List<Contacts>();
            currentBookName = bookName;
            return true;
        }

        // Select an existing address book and set it as current
        public bool SelectAddressBook(string bookName)
        {
            if (addressBooks.ContainsKey(bookName))
            {
                currentBookName = bookName;
                return true;
            }
            return false;
        }

        // Add a contact to the current address book
        public void AddContact(Contacts contact)
        {
            if (currentBookName == null)
            {
                Console.WriteLine("No Address Book selected.");
                return;
            }

            var list = addressBooks[currentBookName];
            foreach (var c in list)
            {
                if (c.FirstName.ToLower() == contact.FirstName.ToLower())
                {
                    Console.WriteLine("Contact with this First Name already exists in current Address Book.");
                    return;
                }
            }

            list.Add(contact);
            Console.WriteLine("Contact added successfully.");
        }

        // Get all contacts from the current address book
        public List<Contacts> GetAllContacts()
        {
            if (currentBookName == null)
                return new List<Contacts>();

            return new List<Contacts>(addressBooks[currentBookName]);
        }

        // Update a contact in the current address book
        public void UpdateAddressBook(string firstName, string uLastName, string uAddress, string uCity, string uState, string uZipcode, string uEmail)
        {
            if (currentBookName == null)
            {
                Console.WriteLine("No Address Book selected.");
                return;
            }

            var list = addressBooks[currentBookName];
            Contacts contact = null;

            foreach (var c in list)
            {
                if (c.FirstName.ToLower() == firstName.ToLower())
                {
                    contact = c;
                    break; 
                }
            }

            if (contact != null)
            {
                contact.LastName = uLastName;
                contact.Address = uAddress;
                contact.City = uCity;
                contact.State = uState;
                contact.ZipCode = uZipcode;
                contact.Email = uEmail;
                Console.WriteLine("Contact updated successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        // Delete a contact from the current address book
        public void DeletePerson(string firstName)
        {
            if (currentBookName == null)
            {
                Console.WriteLine("No Address Book selected.");
                return;
            }

            var list = addressBooks[currentBookName];
            Contacts contact = null;

            foreach (var c in list)
            {
                if (c.FirstName.ToLower() == firstName.ToLower())
                {
                    contact = c;
                    break;
                }
            }

            if (contact != null)
            {
                list.Remove(contact);
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        // List all address book names
        public List<string> GetAllAddressBookNames()
        {
            return new List<string>(addressBooks.Keys);
        }
    }
}
