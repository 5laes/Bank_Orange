using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Bank_Orange
{
    class BankSystem
    {
        int InLoggedUserIndex;

        private Dictionary<int, Person> PersonDictionary = new Dictionary<int, Person>();
        private Dictionary<int, BankAccount> AccountDictionary = new Dictionary<int, BankAccount>();

        public void Login()
        {
            for (int Attempts = 0; Attempts <= 3; Attempts++)
            {
                if (Attempts < 3)
                {
                    Console.Clear();
                    Console.Write("Enter Your Username: ");
                    string Name = Console.ReadLine();
                    Console.Write("\nEnter Your Password: ");
                    string Password = Console.ReadLine();

                    foreach (KeyValuePair<int, Person> item in PersonDictionary)
                    {
                        if (item.Value.Name == Name && item.Value.Password == Password)
                        {
                            if (item.Value.IsAdmin == true)
                            {
                                AdminMenu();
                            }
                            else
                            {
                                CustomerMenu();
                            }
                        }

                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        //Creates a admin profile 
        //Call this method when the program starts
        public void CreateAdmin()
        {
            Admin admin = new Admin();
            PersonDictionary.Add(PersonDictionary.Count, admin);
            BankAccount newBankAccount = new BankAccount();
            AccountDictionary.Add(AccountDictionary.Count, newBankAccount);
        }

        //Creates a customer profile with input from the customer
        public void CreateUserAccount()
        {
            Console.Clear();
            Console.Write("\n\tName: ");
            string name = Console.ReadLine();
            Console.Write("\n\tPassword: ");
            string password = Console.ReadLine();
            Customer newCustomer = new Customer(name, password);
            PersonDictionary.Add(PersonDictionary.Count, newCustomer);
            BankAccount newBankAccount = new BankAccount();
            AccountDictionary.Add(AccountDictionary.Count, newBankAccount);
            AdminMenu();
        }


        //Menyn som visas när man är inloggad
        public void CustomerMenu()
        {
            Console.Clear();
            Console.Write("\n\t[1]Add new bank account" +
                "\n\t[2]Show accounts" +
                "\n\t[3]Logout" +
                "\n\t: ");
            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    CustomerMenu();
                    break;
            }
        }

        public void AdminMenu()
        {
            Console.Clear();
            Console.Write("\n\t[1]Add new bank account" +
                "\n\t[2]Show accounts" +
                "\n\t[3]Create account" +
                "\n\t[4]Logout" +
                "\n\t: ");
            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    CustomerMenu();
                    break;
            }
        }
    }
}
