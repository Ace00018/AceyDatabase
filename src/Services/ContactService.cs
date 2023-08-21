
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
        }

        public void RemoveContact(string name)
        {
            Contact contactToRemove = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                Console.WriteLine("Contact removed successfully!");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void EditContact(string name, string newPhoneNumber)
        {
            Contact contactToEdit = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (contactToEdit != null)
            {
                contactToEdit.PhoneNumber = newPhoneNumber;
                Console.WriteLine("Contact edited successfully!");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void EditContact(string name, string newName, string newPhoneNumber)
        {
            Contact contactToEdit = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (contactToEdit != null)
            {
                contactToEdit.Name = newName;
                contactToEdit.PhoneNumber = newPhoneNumber;
                Console.WriteLine("Contact edited successfully!");
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

        private string GetContactsFilePath()
        {
            string contactsDirectory = Path.Combine(userDirectory, "RustyDatabase"); // Changed folder name
            Directory.CreateDirectory(contactsDirectory);

            return Path.Combine(contactsDirectory, "contacts.txt");
        }

        internal Contact GetContactByName(string name)
        {
            return contacts.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
