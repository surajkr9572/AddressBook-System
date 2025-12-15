using AddressBook.Entity;
using AddressBook.Interface;
using AddressBook.Service;

namespace AddressBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAddressBook addressbook=new AddressBookService();
            Contacts contact = new Contacts();
            Console.WriteLine("Enter First Name : ");
            contact.FirstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name : ");
            contact.LastName = Console.ReadLine();

            Console.WriteLine("Enter Address : ");
            contact.Address = Console.ReadLine();

            Console.WriteLine("Enter City : ");
            contact.City = Console.ReadLine();

            Console.WriteLine("Enter State: ");
            contact.State = Console.ReadLine();

            Console.Write("Enter Zip Code: ");
            contact.ZipCode = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            contact.PhoneNumber = Console.ReadLine();

            Console.Write("Enter Email: ");
            contact.Email = Console.ReadLine();

            addressbook.AddContact(contact);

            Console.WriteLine("Contact added successfully!");
        }
    }
}
