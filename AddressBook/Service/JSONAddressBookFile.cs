using AddressBook.Entity;
using AddressBook.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AddressBook.Service
{
    public class JSONAddressBookFile : IAddressBookFileIO
    {
        private const string path= @"E:\Project3\AddressBook\AddressBook.json";
        public async Task WriteToFileAsync(Dictionary<string, List<Contacts>> addressBooks) 
        {
            var option = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json=JsonSerializer.Serialize(addressBooks, option);
            await File.WriteAllTextAsync(path, json);
        }

        //Read

        public Dictionary<string, List<Contacts>> ReadFromFile()
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "{}");
                return new Dictionary<string, List<Contacts>>();
            }
            string json= File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
            {
                return new Dictionary<string, List<Contacts>>();
            }
            var data=JsonSerializer.Deserialize<Dictionary<string,List<Contacts>>>  (json);
            return data ?? new Dictionary<string, List<Contacts>>();
        }
    }

    
}
