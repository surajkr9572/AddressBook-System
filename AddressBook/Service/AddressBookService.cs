using AddressBook.Entity;
using AddressBook.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace AddressBook.Service
{
    public class AddressBookService : IAddressBook
    {
        private readonly IAddressBookFileIO fileIO;
        public Dictionary<string, List<Contacts>> addressBooks;
        private string currentBookName;

        private Dictionary<string, List<Contacts>> cityPersonMap = new();
        private Dictionary<string, List<Contacts>> statePersonMap = new();

        //  CONSTRUCTOR
        public AddressBookService()
        {
            fileIO = new JSONAddressBookFile();
            addressBooks = fileIO.ReadFromFile(); // UC-13
        }

        public bool CreateAddressBook(string bookName)
        {
            if (addressBooks.ContainsKey(bookName))
                return false;

            addressBooks[bookName] = new List<Contacts>();
            currentBookName = bookName;
            fileIO.WriteToFile(addressBooks);
            return true;
        }

        public bool SelectAddressBook(string bookName)
        {
            if (!addressBooks.ContainsKey(bookName))
                return false;

            currentBookName = bookName;
            return true;
        }

        public bool AddContact(Contacts contact)
        {
            if (currentBookName == null)
            {
                Console.WriteLine("No AddressBook selected.");
                return false;
            }

            var list = addressBooks[currentBookName];

            if (list.Any(c => c.Equals(contact)))
            {
                Console.WriteLine("Contact already exists.");
                return false;
            }

            list.Add(contact);
            fileIO.WriteToFile(addressBooks);

            if (!cityPersonMap.ContainsKey(contact.City))
                cityPersonMap[contact.City] = new List<Contacts>();
            cityPersonMap[contact.City].Add(contact);

            if (!statePersonMap.ContainsKey(contact.State))
                statePersonMap[contact.State] = new List<Contacts>();
            statePersonMap[contact.State].Add(contact);

            return true;
        }

        public List<Contacts> GetAllContacts()
        {
            if (currentBookName == null)
                return new List<Contacts>();

            return new List<Contacts>(addressBooks[currentBookName]);
        }

        public void UpdateAddressBook(string firstName, string lastName,
            string address, string city, string state, string zip, string email)
        {
            if (currentBookName == null) return;

            var contact = addressBooks[currentBookName]
                .FirstOrDefault(c => c.FirstName.Equals(firstName,
                    StringComparison.OrdinalIgnoreCase));

            if (contact == null) return;

            contact.LastName = lastName;
            contact.Address = address;
            contact.City = city;
            contact.State = state;
            contact.ZipCode = zip;
            contact.Email = email;

            fileIO.WriteToFile(addressBooks);
        }

        public void DeletePerson(string firstName)
        {
            if (currentBookName == null) return;

            var list = addressBooks[currentBookName];
            var contact = list.FirstOrDefault(c =>
                c.FirstName.Equals(firstName,
                StringComparison.OrdinalIgnoreCase));

            if (contact == null) return;

            list.Remove(contact);
            fileIO.WriteToFile(addressBooks);
        }

        public List<string> GetAllAddressBookNames()
        {
            return addressBooks.Keys.ToList();
        }
        public List<Contacts> SearchCity(string cityName)
        {
            List<Contacts> result = new List<Contacts>();

            foreach (var entry in addressBooks)
            {
                foreach (Contacts c in entry.Value)
                {
                    if (c.City.ToLower()==cityName.ToLower())
                    {
                        result.Add(c);
                    }
                }
            }

            return result;
        }

        public List<Contacts> SearchState(string stateName)
        {
            List<Contacts> result = new List<Contacts>();

            foreach (var entry in addressBooks)
            {
                foreach (Contacts c in entry.Value)
                {
                    if (c.State != null &&
                        c.State.ToLower() == stateName.ToLower())
                    {
                        result.Add(c);
                    }
                }
            }

            return result;

        }
        //count person in same city
        public int CountCity(string cityName)
        {
            int count = 0;
            if (cityPersonMap.ContainsKey(cityName))
            {
                count= cityPersonMap[cityName].Count;
            }
            return count;
        }
        //count person in same state
        public int CountState(string stateName)
        {
            int count = 0;
            if (statePersonMap.ContainsKey(stateName))
            {
                count= statePersonMap[stateName].Count;
            }
            return count;
        }
        //get all Name in sorting Order
        public List<Contacts> SortByName()
        {
            if (currentBookName == null)
                return new List<Contacts>();

            return new List<Contacts>(addressBooks[currentBookName]);
        }
        // Sorted By City
        public List<Contacts> SortByCity(){
            if(currentBookName==null) return new List<Contacts>();
            return addressBooks[currentBookName].
                OrderBy(x => x.City).ToList();
        }
        public List<Contacts> SortByState()
        {
            if(currentBookName==null)return new List<Contacts>();
            return addressBooks[currentBookName].
                OrderBy(x => x.State).ToList();
        }
        public List<Contacts> SortByZip()
        {
            if (currentBookName == null) return new List<Contacts>();
            return addressBooks[currentBookName].
                OrderBy(x=>x.ZipCode).ToList();
        }

    }
}
