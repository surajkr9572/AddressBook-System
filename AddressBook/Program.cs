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
            Dictionary<string, IAddressBook> addressBooks = new Dictionary<string, IAddressBook>();
            IAddressBook currentBook = null;

            int mainChoice;

            do
            {
                Console.WriteLine("\n==== MAIN MENU ====");
                Console.WriteLine("1. Create Address Book");
                Console.WriteLine("2. Select Address Book");
                Console.WriteLine("3. Exit");

                if (!int.TryParse(Console.ReadLine(), out mainChoice))
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                switch (mainChoice)
                {
                    case 1: // Create
                        Console.Write("Enter New Address Book Name: ");
                        string newBookName = Console.ReadLine();

                        if (addressBooks.ContainsKey(newBookName))
                        {
                            Console.WriteLine("Address Book already exists!");
                        }
                        else
                        {
                            currentBook = new AddressBookService();           // new service object
                            if (currentBook.CreateAddressBook(newBookName))  // call method to set currentBookName
                            {
                                addressBooks[newBookName] = currentBook;     // store in dictionary
                                Console.WriteLine($"Address Book '{newBookName}' created and selected.");
                                AddressBookMenu(currentBook);
                            }
                        }
                        break;

                    case 2: // Select
                        Console.Write("Enter Address Book Name to Select: ");
                        string selectBookName = Console.ReadLine();

                        if (addressBooks.TryGetValue(selectBookName, out currentBook))
                        {
                            if (currentBook.SelectAddressBook(selectBookName)) // set currentBookName internally
                            {
                                Console.WriteLine($"Address Book '{selectBookName}' selected.");
                                AddressBookMenu(currentBook);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Address Book not found!");
                        }
                        break;


                    case 3:
                        Console.WriteLine("Exiting Application...");
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (mainChoice != 3);
        }
        static void AddressBookMenu(IAddressBook addressbook)
        {
            int choice;

            do
            {
                Console.WriteLine("\n--- ADDRESS BOOK MENU ---");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Edit Contact");
                Console.WriteLine("3. Delete Contact");
                Console.WriteLine("4. View Contacts");
                Console.WriteLine("5. Back to Main Menu");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        

                        Console.Write("First Name: ");
                        string FirstName= Console.ReadLine();
                        Contacts contact = new Contacts(FirstName);
                        Console.Write("Last Name: ");
                        contact.LastName = Console.ReadLine();

                        Console.Write("Address: ");
                        contact.Address = Console.ReadLine();

                        Console.Write("City: ");
                        contact.City = Console.ReadLine();

                        Console.Write("State: ");
                        contact.State = Console.ReadLine();

                        Console.Write("Zip: ");
                        contact.ZipCode = Console.ReadLine();

                        Console.Write("Phone Number: ");
                        contact.PhoneNumber = Console.ReadLine();

                        Console.Write("Email: ");
                        contact.Email = Console.ReadLine();

                        addressbook.AddContact(contact);
                        Console.WriteLine("Contact Added!");
                        break;

                    case 2:
                        Console.Write("Enter First Name to Edit: ");
                        string Name = Console.ReadLine();

                        Console.Write("New Last Name: ");
                        string editlastname = Console.ReadLine();

                        Console.Write("New Address: ");
                        string editaddr = Console.ReadLine();

                        Console.Write("New City: ");
                        string editcity = Console.ReadLine();

                        Console.Write("New State: ");
                        string editstate = Console.ReadLine();

                        Console.Write("New Zip: ");
                        string editzip = Console.ReadLine();

                        Console.Write("New Email: ");
                        string editemail = Console.ReadLine();

                        addressbook.UpdateAddressBook(Name, editlastname, editaddr, editcity, editstate, editzip, editemail);
                        break;

                    case 3:
                        Console.Write("Enter First Name to Delete: ");
                        string delName = Console.ReadLine();
                        addressbook.DeletePerson(delName);
                        break;

                    case 4:
                        var list = addressbook.GetAllContacts();

                        Console.WriteLine(
                            $"{"First Name",-12} {"Last Name",-12} {"City",-10} {"State",-8} {"Zip",-8} {"Phone",-12} {"Email"}");
                        Console.WriteLine(new string('-', 80));

                        foreach (var c in list)
                        {
                            Console.WriteLine(
                                $"{c.FirstName,-12} {c.LastName,-12} {c.City,-10} {c.State,-8} {c.ZipCode,-8} {c.PhoneNumber,-12} {c.Email}");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Returning to Main Menu...");
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (choice != 5);
        }
    }
}
