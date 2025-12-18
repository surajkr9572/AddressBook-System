using AddressBook.Entity;
using AddressBook.Exceptions;
using AddressBook.Interface;
using AddressBook.Service;
using System;
using System.Collections.Generic;

namespace AddressBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string banner =
                "   ___     __   __                  ___            __  \r\n  / _ |___/ /__/ /______ ___ ___   / _ )___  ___  / /__\r\n / __ / _  / _  / __/ -_|_-<(_-<  / _  / _ \\/ _ \\/  '_/\r\n/_/ |_\\_,_/\\_,_/_/  \\__/___/___/ /____/\\___/\\___/_/\\_\\ \r\n";
            Console.WriteLine(banner);
            IAddressBook service = new AddressBookService();

            int mainChoice=0;
            
            do
            {
                try
                {
                    Console.WriteLine("\n==== MAIN MENU ====");
                    Console.WriteLine("1. Create Address Book");
                    Console.WriteLine("2. Select Address Book");
                    Console.WriteLine("3. Search Using City (Across All Address Books)");
                    Console.WriteLine("4. Search Using State (Across All Address Books)");
                    Console.WriteLine("5. Count By City (Across All Address Books)");
                    Console.WriteLine("6. Count By State (Across All Address Books)");
                    Console.WriteLine("7. Exit");

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

                            if (!service.CreateAddressBook(newBookName))
                            {
                                throw new AddressBookExists("Address Book already exists!");
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(newBookName))
                                {
                                    throw new InvalidAddressBook("Enter Valid AddressBook Name : ");
                                }
                                Console.WriteLine($"Address Book '{newBookName}' created and selected.");
                                AddressBookMenu(service);
                            }
                            break;

                        case 2: // Select
                            Console.Write("Enter Address Book Name to Select: ");
                            string selectBookName = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(selectBookName))
                            {
                                throw new InvalidAddressBook("Enter Valid AddressBook Name : ");
                            }
                            else
                            {
                                if (service.SelectAddressBook(selectBookName))
                                {
                                    Console.WriteLine($"Address Book '{selectBookName}' selected.");
                                    AddressBookMenu(service);
                                }
                                else
                                {
                                    throw new AddressBookNotExists("Address Book not found!");
                                }
                            }
                            break;

                        case 3: // Search Using City across all books
                            Console.Write("Enter City Name : ");
                            string cityName = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(cityName))
                            {
                                throw new CityNameInvalid("City Name Invalid. Enter valid City Name.");
                            }

                            var cityList = service.SearchCity(cityName);
                            if (cityList.Count == 0)
                            {
                                Console.WriteLine($"No persons found in city '{cityName}'.");
                            }
                            else
                            {
                                Console.WriteLine($"\nPersons in city '{cityName}':");
                                Console.WriteLine($"{"First Name",-12} {"Last Name",-12} {"City",-10} {"State",-8} {"Zip",-8} {"Phone",-12} {"Email"}");
                                Console.WriteLine(new string('-', 80));
                                foreach (var c in cityList)
                                {
                                    Console.WriteLine($"{c.FirstName} {c.LastName} - {c.Address} - {c.City} - {c.State} - {c.ZipCode} - {c.PhoneNumber} - {c.Email}");
                                }
                            }
                            break;

                        case 4: // Search Using State across all books
                            Console.Write("Enter State Name : ");
                            string stateName = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(stateName))
                            {
                                throw new StateNameInvalid("State Name Invalid. Enter valid State Name.");
                            }

                            var stateList = service.SearchState(stateName);
                            if (stateList.Count == 0)
                            {
                                Console.WriteLine($"No persons found in state '{stateName}'.");
                            }
                            else
                            {
                                Console.WriteLine($"\nPersons in state '{stateName}':");
                                Console.WriteLine($"{"First Name",-12} {"Last Name",-12} {"City",-10} {"State",-8} {"Zip",-8} {"Phone",-12} {"Email"}");
                                Console.WriteLine(new string('-', 80));
                                foreach (var c in stateList)
                                {
                                    Console.WriteLine($"{c.FirstName} {c.LastName} - {c.Address} - {c.City} - {c.State} - {c.ZipCode} - {c.PhoneNumber} - {c.Email}");
                                }
                            }
                            break;
                        case 5: //Count How many person are in same city
                            Console.Write("Enter City Name : ");
                            string CityName_Count=Console.ReadLine();
                            if(string.IsNullOrWhiteSpace(CityName_Count))
                            {
                                throw new CityNameInvalid("City Name Invalid. Enter valid City Name.");
                            }
                            int cityCount= service.CountCity(CityName_Count);
                            Console.WriteLine($"{cityCount} Present is {CityName_Count} City.");
                            break;
                        case 6: //Count How many person are in same state
                            Console.Write("Enter State Name : ");
                            string StateName_Count = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(StateName_Count))
                            {
                                throw new CityNameInvalid("City Name Invalid. Enter valid City Name.");
                            }
                            int StateCount = service.CountState(StateName_Count);
                            Console.WriteLine($"{StateCount} Present is {StateName_Count} State.");
                            break;
                        case 7:
                            Console.WriteLine("Exiting Application...");
                            break;

                        default:
                            Console.WriteLine("Invalid option");
                           
                            break;
                    }
                }
                catch (InvalidAddressBook ex)
                {
                    PrintError(ex.Message);
                }
                catch (AddressBookExists ex)
                {
                    PrintError(ex.Message);
                }
                catch (AddressBookNotExists ex)
                {
                    PrintError(ex.Message);
                }
                catch (CityNameInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (StateNameInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (Exception ex)
                {
                    PrintError("Unexpected Error : " + ex.Message);
                }


            } while (mainChoice != 7); 
        }

        static void AddressBookMenu(IAddressBook addressbook)
        {
            int choice=0;

            do
            {
                try
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
                            string FirstName = ReadRequiredInput(
                                "First Name: ",
                                "First Name Invalid. Enter valid First Name."
                            );

                            Contacts contact = new Contacts(FirstName);

                            contact.LastName = ReadRequiredInput(
                                "Last Name: ",
                                "Last Name Invalid. Enter valid Last Name."
                            );

                            contact.Address = ReadRequiredInput(
                                "Address: ",
                                "Address Invalid. Enter valid Address."
                            );

                            contact.City = ReadRequiredInput(
                                "City: ",
                                "City Name Invalid. Enter valid City Name."
                            );

                            contact.State = ReadRequiredInput(
                                "State: ",
                                "State Name Invalid. Enter valid State Name."
                            );

                            contact.ZipCode = ReadRequiredInput(
                                "Zip: ",
                                "Zip Code Invalid. Enter valid Zip Code."
                            );

                            contact.PhoneNumber = ReadRequiredInput(
                                "Phone Number: ",
                                "Phone Number Invalid. Enter valid Phone Number."
                            );

                            contact.Email = ReadRequiredInput(
                                "Email: ",
                                "Email Invalid. Enter valid Email."
                            );

                            addressbook.AddContact(contact);
                            Console.WriteLine("Contact Added Successfully!");
                            break;


                        case 2:
                            string Name = ReadRequiredInput(
                                "Enter First Name to Edit: ",
                                "First Name Invalid. Enter valid First Name."
                            );

                            string editlastname = ReadRequiredInput(
                                "New Last Name: ",
                                "Last Name Invalid. Enter valid Last Name."
                            );

                            string editaddr = ReadRequiredInput(
                                "New Address: ",
                                "Address Invalid. Enter valid Address."
                            );

                            string editcity = ReadRequiredInput(
                                "New City: ",
                                "City Name Invalid. Enter valid City Name."
                            );

                            string editstate = ReadRequiredInput(
                                "New State: ",
                                "State Name Invalid. Enter valid State Name."
                            );

                            string editzip = ReadRequiredInput(
                                "New Zip: ",
                                "Zip Code Invalid. Enter valid Zip Code."
                            );

                            string editemail = ReadRequiredInput(
                                "New Email: ",
                                "Email Invalid. Enter valid Email."
                            );

                            addressbook.UpdateAddressBook(Name, editlastname, editaddr, editcity, editstate, editzip, editemail);

                            Console.WriteLine("Contact Updated Successfully!");
                            break;


                        case 3:
                            Console.Write("Enter First Name to Delete: ");
                            string delName = Console.ReadLine();
                            addressbook.DeletePerson(delName);
                            break;

                        case 4:
                            var list = addressbook.GetAllContacts();

                            Console.WriteLine($"{"First Name",-12} {"Last Name",-12} {"City",-10} {"State",-8} {"Zip",-8} {"Phone",-12} {"Email"}");
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
                }
                catch (FirstNameInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (LastNameInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (AddressInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (CityNameInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (StateNameInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (ZipInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (PhoneInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (EmailInvalid ex)
                {
                    PrintError(ex.Message);
                }
                catch (Exception ex)
                {
                    PrintError("Unexpected Error : " + ex.Message);
                }

            } while (choice != 5);
        }
        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR : " + message);
            Console.ResetColor();
        }
        static string ReadRequiredInput(string label, string errorMessage)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                PrintError(errorMessage);
            }
        }


    }

}
