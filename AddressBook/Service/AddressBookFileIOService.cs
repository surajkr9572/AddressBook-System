using AddressBook.Entity;
using AddressBook.Interface;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AddressBook.Service
{
    public class AddressBookFileIOService : IAddressBookFileIO
    {
        private const string filePath =
            @"E:\Project3\AddressBook\AddressBook.txt";

        public Dictionary<string, List<Contacts>> ReadFromFile()
        {
            Dictionary<string, List<Contacts>> addressBooks =
                new Dictionary<string, List<Contacts>>();

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                return addressBooks;
            }

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var data = line.Split(',');

                string bookName = data[0];

                Contacts contact = new Contacts(data[1])
                {
                    LastName = data[2],
                    Address = data[3],
                    City = data[4],
                    State = data[5],
                    ZipCode = data[6],
                    PhoneNumber = data[7],
                    Email = data[8]
                };

                if (!addressBooks.ContainsKey(bookName))
                    addressBooks[bookName] = new List<Contacts>();

                if (!addressBooks[bookName].Any(c => c.Equals(contact)))
                    addressBooks[bookName].Add(contact);
            }

            return addressBooks;
        }

        public void WriteToFile(Dictionary<string, List<Contacts>> addressBooks)
        {
            using StreamWriter writer = new StreamWriter(filePath, false);

            foreach (var book in addressBooks)
            {
                foreach (var c in book.Value)
                {
                    writer.WriteLine(
                        $"{book.Key},{c.FirstName},{c.LastName},{c.Address},{c.City},{c.State},{c.ZipCode},{c.PhoneNumber},{c.Email}"
                    );
                }
            }
        }
    }
}
