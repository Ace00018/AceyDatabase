# Rusty Database - Contact Management System

Rusty Database is a simple Contact Management System console application built in C# that allows users to manage their contacts. It provides features to add, remove, edit, and display contacts. Contacts are stored in a text file within a user-specific directory.

## Features

- Add a new contact with a name and phone number.
- Remove an existing contact by name.
- Edit an existing contact's phone number (and optionally, name).
- Display a list of all contacts.
- Data is stored persistently in a text file.
- Allows dashes, brackets, and other characters in phone numbers.

## Getting Started

1. Clone the repository to your local machine.
2. Open the project in Visual Studio or your preferred C# development environment.
3. Build and run the application.

## Releases

You can also download pre-built releases of Rusty Database from the [Releases](https://github.com/yoRustic/RustyDatabase/releases) section. This allows you to run the application without needing to build it from source.

## Usage

1. Choose options from the main menu to manage your contacts:
   - **Add Contact:** Add a new contact with a name and phone number.
   - **Remove Contact:** Remove an existing contact by providing the name.
   - **Edit Contact:** Edit an existing contact's phone number and optionally the name.
   - **Show Contacts:** Display a list of all saved contacts.
   - **Exit:** Save the contacts and exit the application.

2. Follow the prompts to input contact details. The application provides validation for name and phone number inputs.

## Data Storage

Contacts are stored in a text file named `contacts.txt` within a directory named `RustyDatabase` located in your user's "My Documents" folder. Each contact is stored in a CSV format: `Name,PhoneNumber`.

## Contributing

Contributions to improve the project are welcome! You can fork the repository, make your changes, and submit a pull request.

## License

This project is licensed under the [MIT License].