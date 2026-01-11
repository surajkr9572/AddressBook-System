using AddressBook.Entity;
using AddressBook.Interface;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Service
{
    public class DatabaseAddressBookService : IAddressBookFileIO
    {
        private readonly string connectionString =
            "Data Source=SURAJ;Initial Catalog=AddressBook;Integrated Security=True;Encrypt=False";

        // Read data from database and rebuild AddressBook structure
        public Dictionary<string, List<Contacts>> ReadFromFile()
        {
            Dictionary<string, List<Contacts>> addressBooks = new();

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "SELECT BookName, FirstName, LastName, Address, City, State, ZipCode, PhoneNumber, Email FROM Contacts";
            using SqlCommand cmd = new SqlCommand(query, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string bookName = reader.GetString(0);

                Contacts contact = new Contacts(reader.GetString(1))
                {
                    LastName = reader.GetString(2),
                    Address = reader.GetString(3),
                    City = reader.GetString(4),
                    State = reader.GetString(5),
                    ZipCode = reader.GetString(6),
                    PhoneNumber = reader.GetString(7),
                    Email = reader.GetString(8)
                };

                if (!addressBooks.ContainsKey(bookName))
                    addressBooks[bookName] = new List<Contacts>();

                addressBooks[bookName].Add(contact);
            }

            return addressBooks;
        }

        // Write all AddressBooks into database
        public async Task WriteToFileAsync(Dictionary<string, List<Contacts>> addressBooks)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            // Clear old data to prevent duplicates
            using (SqlCommand clearCmd = new SqlCommand("DELETE FROM Contacts", conn))
            {
                await clearCmd.ExecuteNonQueryAsync();
            }

            foreach (var book in addressBooks)
            {
                foreach (var c in book.Value)
                {
                    using SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO Contacts 
                          (BookName, FirstName, LastName, Address, City, State, ZipCode, PhoneNumber, Email)
                          VALUES (@Book,@FName,@LName,@Address,@City,@State,@ZIP,@Phone,@Email)",
                        conn);

                    cmd.Parameters.AddWithValue("@Book", book.Key);
                    cmd.Parameters.AddWithValue("@FName", c.FirstName);
                    cmd.Parameters.AddWithValue("@LName", c.LastName);
                    cmd.Parameters.AddWithValue("@Address", c.Address);
                    cmd.Parameters.AddWithValue("@City", c.City);
                    cmd.Parameters.AddWithValue("@State", c.State);
                    cmd.Parameters.AddWithValue("@ZIP", c.ZipCode);
                    cmd.Parameters.AddWithValue("@Phone", c.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", c.Email);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
