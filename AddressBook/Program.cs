using AddressBook.Entity;
using AddressBook.Interface;
using AddressBook.Service;
using System;

namespace AddressBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string banner =
                "   ___     __   __                  ___            __  \r\n  / _ |___/ /__/ /______ ___ ___   / _ )___  ___  / /__\r\n / __ / _  / _  / __/ -_|_-<(_-<  / _  / _ \\/ _ \\/  '_/\r\n/_/ |_\\_,_/\\_,_/_/  \\__/___/___/ /____/\\___/\\___/_/\\_\\ \r\n";
            Console.WriteLine(banner);
            IAddressBook addressbook = new AddressBookService();
            int val;

            do
            {
                Console.WriteLine("\n------ Address Book Menu ------");
                Console.WriteLine("Add Enter 1");
                Console.WriteLine("Edit Enter 2");
                Console.WriteLine("Delete Enter 3");
                Console.WriteLine("Get All List Enter 4");
                Console.WriteLine("Exit Enter 5");

                if (!int.TryParse(Console.ReadLine(), out val))
                {
                    Console.WriteLine("Invalid input. Enter number only.");
                    continue;
                }

                switch (val)
                {
                    case 1:
                        Contacts contact = new Contacts();

                        Console.Write("Enter First Name: ");
                        contact.FirstName = Console.ReadLine();

                        Console.Write("Enter Last Name: ");
                        contact.LastName = Console.ReadLine();

                        Console.Write("Enter Address: ");
                        contact.Address = Console.ReadLine();

                        Console.Write("Enter City: ");
                        contact.City = Console.ReadLine();

                        Console.Write("Enter State: ");
                        contact.State = Console.ReadLine();

                        Console.Write("Enter Zip Code: ");
                        contact.ZipCode = Console.ReadLine();

                        Console.Write("Enter Phone Number: ");
                        contact.PhoneNumber = Console.ReadLine();

                        Console.Write("Enter Email: ");
                        contact.Email = Console.ReadLine();

                        addressbook.AddContact(contact);
                        Console.WriteLine("Contact Added Successfully!");
                        break;

                    case 2:
                        Console.Write("Enter First Name to Edit: ");
                        string editFirstName = Console.ReadLine();

                        Console.Write("Enter New Last Name: ");
                        string editLastName = Console.ReadLine();

                        Console.Write("Enter New Address: ");
                        string editAddress = Console.ReadLine();

                        Console.Write("Enter New City: ");
                        string editCity = Console.ReadLine();

                        Console.Write("Enter New State: ");
                        string editState = Console.ReadLine();

                        Console.Write("Enter New Zip Code: ");
                        string editZip = Console.ReadLine();

                        Console.Write("Enter New Email: ");
                        string editEmail = Console.ReadLine();

                        addressbook.UpdateAddressBook(
                            editFirstName,
                            editLastName,
                            editAddress,
                            editCity,
                            editState,
                            editZip,
                            editEmail
                        );

                        Console.WriteLine("Contact Updated Successfully!");
                        break;

                    case 3:
                        Console.Write("Enter First Name to Delete: ");
                        string deleteName = Console.ReadLine();
                        addressbook.DeletePerson(deleteName);
                        Console.WriteLine("Contact Deleted Successfully!");
                        break;

                    case 4:
                        var list = addressbook.GetAllContacts();

                        Console.WriteLine(
                               $"{"First Name",-12} {"Last Name",-12} {"City",-10} {"State",-8} {"Zip",-8} {"Phone",-12} {"Email"}");

                        Console.WriteLine(new string('-', 80));

                        foreach (var e in list)
                        {
                            Console.WriteLine(
                                $"{e.FirstName,-12} {e.LastName,-12} {e.City,-10} {e.State,-8} {e.ZipCode,-8} {e.PhoneNumber,-12} {e.Email}");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Exiting Address Book...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter 1 to 5.");
                        break;
                }

            } while (val != 5);
        }
    }
}
