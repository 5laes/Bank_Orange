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

                }
                else
                {
                    Environment.Exit(0);
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
            //create a method that shows the admin menu
            //AdminMenu();
        }
    }
}
