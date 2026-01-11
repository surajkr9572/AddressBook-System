using AddressBook.Entity;
using AddressBook.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Service
{
    public class CSVAddressBookFileIOService: IAddressBookFileIO
    {
        //write in CSV
        private const string filePath =@"E:\Project3\AddressBook\AddressBook.csv";

        public Dictionary<string, List<Contacts>> ReadFromFile()
        {
            Dictionary<string, List<Contacts>> addressBooks =
                new Dictionary<string, List<Contacts>>();

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                return addressBooks;
            }

            var lines = File.ReadAllLines(filePath);

            if (lines.Length <= 2)
                return addressBooks;

            for (int i = 2; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string bookName = line.Substring(0, 12).Trim();
                string firstName = line.Substring(12, 12).Trim();
                string lastName = line.Substring(24, 12).Trim();
                string address = line.Substring(36, 12).Trim();
                string city = line.Substring(48, 10).Trim();
                string state = line.Substring(58, 8).Trim();
                string zip = line.Substring(66, 8).Trim();
                string phone = line.Substring(74, 12).Trim();
                string email = line.Substring(86).Trim();

                Contacts contact = new Contacts(firstName)
                {
                    LastName = lastName,
                    Address = address,
                    City = city,
                    State = state,
                    ZipCode = zip,
                    PhoneNumber = phone,
                    Email = email
                };

                if (!addressBooks.ContainsKey(bookName))
                    addressBooks[bookName] = new List<Contacts>();
                if (!addressBooks[bookName].Any(c => c.Equals(contact)))
                    addressBooks[bookName].Add(contact);
            }

            return addressBooks;
        }
        
        public async Task WriteToFileAsync(Dictionary<string, List<Contacts>> addressBooks)
        {  

            await using StreamWriter streamwriter = new StreamWriter(filePath,false);
            streamwriter.WriteLine($"{"AddressBook",-12} {"First Name",-12} {"Last Name",-12} {"Address",-12}{"City",-10} {"State",-8} {"Zip",-8} {"Phone",-12} {"Email"}");
            streamwriter.WriteLine(new string('-', 104));
            foreach (var list in addressBooks)
            {
                foreach(var contact in list.Value)
                {
                    streamwriter.WriteLine($"{list.Key,-12}{contact.FirstName,-12}{contact.LastName,-12}{contact.Address,-12}{contact.City,-10}{contact.State,-8}{contact.ZipCode,-8}{contact.PhoneNumber,-12}{contact.Email}");
                }
            }
        }

    }
}
