using AddressBook.Entity;
using AddressBook.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Service
{
    public class AddressBookService : IAddressBook
    {
        private readonly List<IAddressBookFileIO> fileServices;
        public Dictionary<string, List<Contacts>> addressBooks;
        private string currentBookName;

        private Dictionary<string, List<Contacts>> cityPersonMap = new();
        private Dictionary<string, List<Contacts>> statePersonMap = new();

        public AddressBookService(List<IAddressBookFileIO> services)
        {
            fileServices = services;
            addressBooks = fileServices.First().ReadFromFile();
        }

        public bool CreateAddressBook(string bookName)
        {
            if (addressBooks.ContainsKey(bookName))
                return false;

            addressBooks[bookName.ToLower()] = new List<Contacts>();
            currentBookName = bookName;
            WriteContactInAsyncForm();
            return true;
        }

        public bool SelectAddressBook(string bookName)
        {
            if (!addressBooks.ContainsKey(bookName.ToLower()))
                return false;

            currentBookName = bookName.ToLower();
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
            WriteContactInAsyncForm();

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

            WriteContactInAsyncForm();
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
            WriteContactInAsyncForm();
        }

        public List<string> GetAllAddressBookNames()
        {
            return addressBooks.Keys.ToList();
        }

        public List<Contacts> SearchCity(string cityName)
        {
            List<Contacts> result = new();

            foreach (var entry in addressBooks)
                result.AddRange(entry.Value.Where(c =>
                    c.City != null &&
                    c.City.Equals(cityName, StringComparison.OrdinalIgnoreCase)));

            return result;
        }

        public List<Contacts> SearchState(string stateName)
        {
            List<Contacts> result = new();

            foreach (var entry in addressBooks)
                result.AddRange(entry.Value.Where(c =>
                    c.State != null &&
                    c.State.Equals(stateName, StringComparison.OrdinalIgnoreCase)));

            return result;
        }

        public int CountCity(string cityName)
        {
            return addressBooks
                .SelectMany(book => book.Value)
                .Count(c =>
                    c.City != null &&
                    c.City.Equals(cityName, StringComparison.OrdinalIgnoreCase));
        }

        public int CountState(string stateName)
        {
            return addressBooks
                .SelectMany(book => book.Value)
                .Count(c =>
                    c.State != null &&
                    c.State.Equals(stateName, StringComparison.OrdinalIgnoreCase));
        }


        public List<Contacts> SortByName()
        {
            if (currentBookName == null) return new();
            return new List<Contacts>(addressBooks[currentBookName]);
        }

        public List<Contacts> SortByCity() =>
            currentBookName == null ? new() :
            addressBooks[currentBookName].OrderBy(x => x.City).ToList();

        public List<Contacts> SortByState() =>
            currentBookName == null ? new() :
            addressBooks[currentBookName].OrderBy(x => x.State).ToList();

        public List<Contacts> SortByZip() =>
            currentBookName == null ? new() :
            addressBooks[currentBookName].OrderBy(x => x.ZipCode).ToList();

        private void WriteContactInAsyncForm()
        {
            Task.Run(async () =>
            {
                foreach (var service in fileServices)
                {
                    await service.WriteToFileAsync(addressBooks);
                }
            });
        }
    }
}
