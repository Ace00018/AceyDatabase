

using System;
using System.Linq;
using ContactManagementSystem.Models;
using ContactManagementSystem.Services;

namespace ContactManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactService contactService = new ContactService();
            contactService.LoadContacts();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Contact Management System");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Remove Contact");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Show Contacts");
                Console.WriteLine("5. Exit");

                int choice = GetChoice();

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        AddContact(contactService);
                        break;
                    case 2:
                        Console.Clear();
                        RemoveContact(contactService);
                        break;
                    case 3:
                        Console.Clear();
                        EditContact(contactService);
                        break;
                    case 4:
                        Console.Clear();
                        ShowContacts(contactService);
                        break;
                    case 5:
                        contactService.SaveContacts();
                        Console.WriteLine("Contacts saved. Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid option.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static int GetChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.Write("Invalid input. Enter your choice: ");
            }
            return choice;
        }

        static void AddContact(ContactService contactService)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            while (!IsValidName(name))
            {
                Console.Write("Invalid name. Please enter a valid name: ");
                name = Console.ReadLine();
            }

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            while (!IsValidPhoneNumber(phoneNumber))
            {
                Console.Write("Invalid phone number. Please enter a valid number: ");
                phoneNumber = Console.ReadLine();
            }

            contactService.AddContact(name, phoneNumber);
        }

        static void RemoveContact(ContactService contactService)
        {
            Console.Write("Enter the name of the contact to remove: ");
            string name = Console.ReadLine();
            contactService.RemoveContact(name);
        }

        static void EditContact(ContactService contactService)
        {
            Console.WriteLine("Do you want to edit the name along with the phone number? (y/n): ");
            string choice = Console.ReadLine();

            Console.Write("Enter the name of the contact to edit: ");
            string name = Console.ReadLine();

            if (choice.ToLower() == "y")
            {
                Console.Write("Enter new Name: ");
                string newName = Console.ReadLine();

                Console.Write("Enter new Phone Number: ");
                string newPhoneNumber = Console.ReadLine();

                while (!IsValidPhoneNumber(newPhoneNumber))
                {
                    Console.Write("Invalid phone number. Please enter a valid number: ");
                    newPhoneNumber = Console.ReadLine();
                }

                contactService.EditContact(name, newName, newPhoneNumber);
            }
            else
            {
                Console.Write("Enter new Phone Number: ");
                string newPhoneNumber = Console.ReadLine();

                while (!IsValidPhoneNumber(newPhoneNumber))
                {
                    Console.Write("Invalid phone number. Please enter a valid number: ");
                    newPhoneNumber = Console.ReadLine();
                }

                contactService.EditContact(name, newPhoneNumber);
            }
        }

        static void ShowContacts(ContactService contactService)
        {
            contactService.ShowContacts();
        }

        static bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
        }

        static bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && phoneNumber.All(c => char.IsDigit(c) || c == '-' || c == '(' || c == ')');
        }
    }
}
