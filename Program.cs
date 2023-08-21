using System;
using ContactManagementSystem.Models;
using ContactManagementSystem.Services;
using System.Linq;

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

                int choice = GetChoice(1, 5);

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

        static int GetChoice(int minValue, int maxValue)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < minValue || choice > maxValue)
            {
                Console.Write($"Invalid input. Enter a number between {minValue} and {maxValue}: ");
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
            Console.Write("Enter the name of the contact to edit: ");
            string name = Console.ReadLine();
            contactService.EditContact(name);
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
