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
            Console.Clear();
            Console.SetWindowPosition(0, 0);
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("  /$$$$$$                                       /$$$$$$                        /$$                           /$$           /$$$$$$$              /$$               /$$                                    \n /$$__  $$                                     /$$__  $$                      | $$                          | $$          | $$__  $$            | $$              | $$                                    \n| $$  \\ $$  /$$$$$$$  /$$$$$$  /$$   /$$      | $$  \\__/  /$$$$$$  /$$$$$$$  /$$$$$$    /$$$$$$   /$$$$$$$ /$$$$$$        | $$  \\ $$  /$$$$$$  /$$$$$$    /$$$$$$ | $$$$$$$   /$$$$$$   /$$$$$$$  /$$$$$$ \n| $$$$$$$$ /$$_____/ /$$__  $$| $$  | $$      | $$       /$$__  $$| $$__  $$|_  $$_/   |____  $$ /$$_____/|_  $$_/        | $$  | $$ |____  $$|_  $$_/   |____  $$| $$__  $$ |____  $$ /$$_____/ /$$__  $$\n| $$__  $$| $$      | $$$$$$$$| $$  | $$      | $$      | $$  \\ $$| $$  \\ $$  | $$      /$$$$$$$| $$        | $$          | $$  | $$  /$$$$$$$  | $$      /$$$$$$$| $$  \\ $$  /$$$$$$$|  $$$$$$ | $$$$$$$$\n| $$  | $$| $$      | $$_____/| $$  | $$      | $$    $$| $$  | $$| $$  | $$  | $$ /$$ /$$__  $$| $$        | $$ /$$      | $$  | $$ /$$__  $$  | $$ /$$ /$$__  $$| $$  | $$ /$$__  $$ \\____  $$| $$_____/\n| $$  | $$|  $$$$$$$|  $$$$$$$|  $$$$$$$      |  $$$$$$/|  $$$$$$/| $$  | $$  |  $$$$/|  $$$$$$$|  $$$$$$$  |  $$$$/      | $$$$$$$/|  $$$$$$$  |  $$$$/|  $$$$$$$| $$$$$$$/|  $$$$$$$ /$$$$$$$/|  $$$$$$$\n|__/  |__/ \\_______/ \\_______/ \\____  $$       \\______/  \\______/ |__/  |__/   \\___/   \\_______/ \\_______/   \\___/        |_______/  \\_______/   \\___/   \\_______/|_______/  \\_______/|_______/  \\_______/\n                               /$$  | $$                                                                                                                                                                  \n                              |  $$$$$$/                                                                                                                                                                  \n                               \\______/                                                                                                                                                                   ");
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
            Console.Write("Enter Name or type 'cancel' to cancel: ");
            string name = Console.ReadLine();

            if (name.ToLower() == "cancel")
            {
                Console.WriteLine("Adding contact canceled.");
                return;
            }

            while (!IsValidName(name))
            {
                Console.Write("Invalid name. Please enter a valid name: ");
                name = Console.ReadLine();
            }

            Console.Write("Enter Phone Number or type 'cancel' to cancel: ");
            string phoneNumber = Console.ReadLine();

            if (phoneNumber.ToLower() == "cancel")
            {
                Console.WriteLine("Adding contact canceled.");
                return;
            }

            while (!IsValidPhoneNumber(phoneNumber))
            {
                Console.Write("Invalid phone number. Please enter a valid number: ");
                phoneNumber = Console.ReadLine();
            }

            contactService.AddContact(name, phoneNumber);
        }

        static void RemoveContact(ContactService contactService)
        {
            ShowContactsForEditing(contactService, "Remove Contact");

            Console.Write("Enter the number of the contact to remove or 0 to cancel: ");
            int contactNumber = GetChoice(0, contactService.GetContacts().Count);

            if (contactNumber != 0)
            {
                Contact contactToRemove = contactService.GetContacts()[contactNumber - 1];
                if (contactToRemove != null)
                {
                    contactService.RemoveContact(contactToRemove.Name);
                    contactService.SaveContacts();
                }
                else
                {
                    Console.WriteLine("Contact not found.");
                }
            }
        }

        static void EditContact(ContactService contactService)
        {
            ShowContactsForEditing(contactService, "Edit Contact");

            Console.Write("Enter the number of the contact to edit or 0 to cancel: ");
            int contactNumber = GetChoice(0, contactService.GetContacts().Count);

            if (contactNumber != 0)
            {
                Contact contactToEdit = contactService.GetContacts()[contactNumber - 1];
                if (contactToEdit != null)
                {
                    Console.WriteLine($"Editing contact: {contactToEdit.Name}, Phone: {contactToEdit.PhoneNumber}");
                    Console.WriteLine("Choose what to edit:");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Phone Number");
                    Console.WriteLine("3. Both");
                    Console.WriteLine("4. Cancel");

                    int editChoice = GetChoice(1, 4);

                    switch (editChoice)
                    {
                        case 1:
                            Console.Write("Enter new Name: ");
                            string newName = Console.ReadLine();
                            contactToEdit.Name = newName;
                            break;
                        case 2:
                            Console.Write("Enter new Phone Number: ");
                            string newPhoneNumber = Console.ReadLine();

                            while (!IsValidPhoneNumber(newPhoneNumber))
                            {
                                Console.Write("Invalid phone number. Please enter a valid number: ");
                                newPhoneNumber = Console.ReadLine();
                            }

                            contactToEdit.PhoneNumber = newPhoneNumber;
                            break;
                        case 3:
                            Console.Write("Enter new Name: ");
                            newName = Console.ReadLine();

                            Console.Write("Enter new Phone Number: ");
                            newPhoneNumber = Console.ReadLine();

                            while (!IsValidPhoneNumber(newPhoneNumber))
                            {
                                Console.Write("Invalid phone number. Please enter a valid number: ");
                                newPhoneNumber = Console.ReadLine();
                            }

                            contactToEdit.Name = newName;
                            contactToEdit.PhoneNumber = newPhoneNumber;
                            break;
                        case 4:
                            Console.WriteLine("Edit canceled.");
                            break;
                    }

                    contactService.SaveContacts();
                }
                else
                {
                    Console.WriteLine("Contact not found.");
                }
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

        static void ShowContactsForEditing(ContactService contactService, string action)
        {
            var contacts = contactService.GetContacts();

            if (contacts.Count > 0)
            {
                Console.WriteLine($"Contacts for {action}:");
                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {contacts[i].Name}");
                }
            }
            else
            {
                Console.WriteLine($"No contacts available for {action.ToLower()}.");
            }
        }
    }
}
