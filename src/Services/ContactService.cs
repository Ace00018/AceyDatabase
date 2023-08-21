using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ContactManagementSystem.Models;

namespace ContactManagementSystem.Services
{
    class ContactService
    {
        private List<Contact> contacts = new List<Contact>();
        private string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public void LoadContacts()
        {
            string filePath = GetContactsFilePath();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        Contact contact = new Contact
                        {
                            Name = parts[0],
                            PhoneNumber = parts[1]
                        };
                        contacts.Add(contact);
                    }
                }
                Console.WriteLine("Contacts loaded.");
            }
            else
            {
                Console.WriteLine("No previous contacts found.");
            }
        }

        public void SaveContacts()
        {
            string filePath = GetContactsFilePath();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Contact contact in contacts)
                {
                    writer.WriteLine($"{contact.Name},{contact.PhoneNumber}");
                }
            }
        }

        public void AddContact(string name, string phoneNumber)
        {
            Contact contact = new Contact
            {
                Name = name,
                PhoneNumber = phoneNumber
            };
            contacts.Add(contact);
            Console.WriteLine("Contact added successfully!");
            SaveContacts();
        }

        public void RemoveContact(string name)
        {
            Contact contactToRemove = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                Console.WriteLine("Contact removed successfully!");
                SaveContacts();
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void EditContact(string name)
        {
            ShowContactsForEditing();

            Console.Write("Enter the number of the contact to edit: ");
            int contactNumber = GetChoice(1, contacts.Count);

            Contact contactToEdit = contacts[contactNumber - 1];
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

                SaveContacts();
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void ShowContacts()
        {
            if (contacts.Count > 0)
            {
                Console.WriteLine("Contacts:");
                foreach (Contact contact in contacts)
                {
                    Console.WriteLine($"Name: {contact.Name}, Phone: {contact.PhoneNumber}");
                }
            }
            else
            {
                Console.WriteLine("No contacts available.");
            }
        }

        public List<Contact> GetContacts()
        {
            return contacts;
        }

        private string GetContactsFilePath()
        {
            string contactsDirectory = Path.Combine(userDirectory, "RustyDatabase");
            Directory.CreateDirectory(contactsDirectory);

            return Path.Combine(contactsDirectory, "contacts.txt");
        }

        private int GetChoice(int minValue, int maxValue)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < minValue || choice > maxValue)
            {
                Console.Write($"Invalid input. Enter a number between {minValue} and {maxValue}: ");
            }
            return choice;
        }

        private void ShowContactsForEditing()
        {
            Console.WriteLine("Choose a contact to edit:");
            for (int i = 0; i < contacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {contacts[i].Name}");
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && phoneNumber.All(c => char.IsDigit(c) || c == '-' || c == '(' || c == ')');
        }
    }
}
