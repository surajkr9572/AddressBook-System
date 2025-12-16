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





            Console.WriteLine("---- Edit Contact ----");

            Console.Write("Enter First Name to Edit: ");
            string EditFirstName1 = Console.ReadLine();

            Console.Write("Enter New Last Name: ");
            string EditLastName1 = Console.ReadLine();

            Console.Write("Enter New Address: ");
            string EditAddress1 = Console.ReadLine();

            Console.Write("Enter New City: ");
            string EditCity1 = Console.ReadLine();

            Console.Write("Enter New State: ");
            string EditState1 = Console.ReadLine();

            Console.Write("Enter New Zip Code: ");
            string EditZipCode1 = Console.ReadLine();

            Console.Write("Enter New Email: ");
            string EditEmail1 = Console.ReadLine();

            addressbook.UpdateAddressBook(
                EditFirstName1,
                EditLastName1,
                EditAddress1,
                EditCity1,
                EditState1,
                EditZipCode1,
                EditEmail1
            );


            Console.WriteLine("Enter Person Name for delete...");
            string deletePerson= Console.ReadLine();
            addressbook.DeletePerson(deletePerson);
        }
    }
}
