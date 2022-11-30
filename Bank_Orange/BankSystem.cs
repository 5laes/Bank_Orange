using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Bank_Orange
{
    class BankSystem
    {
        public decimal SekMultiplierFromEuro;
        public decimal SekMultiplierFromDollar;

        //For saving the users index.
        int InLoggedUserIndex;

        //For saving the users account.
        BankAccount InLoggedUserAccount;

        //Dictionaries of users and users accounts where the key keeps them together.
        private Dictionary<int, Person> PersonDictionary = new Dictionary<int, Person>();
        private Dictionary<int, BankAccount> AccountDictionary = new Dictionary<int, BankAccount>();

        //Login method thats saves the users index if a loggin is successfull.
        public void Login()
        {
            for (int Attempts = 0; Attempts <= 3; Attempts++)
            {
                if (Attempts < 3)
                {
                    Console.Clear();

                    Console.Write("\n\tEnter Your Username: ");
                    string Name = Console.ReadLine();

                    Console.Write("\n\tEnter Your Password: ");
                    string Password = Console.ReadLine();

                    foreach (KeyValuePair<int, Person> item in PersonDictionary)
                    {
                        //checks if the username and password exists and match the database
                        if (item.Value.UserName == Name && item.Value.Password == Password)
                        {
                            InLoggedUserIndex = item.Key;
                            GetUserAccount();
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

        //Gets the inlogged users account from the accountdictionary
        //and assigns the empty bankaccount object to it.
        public void GetUserAccount()
        {
            foreach (KeyValuePair<int, BankAccount> item in AccountDictionary)
            {
                if (item.Key == InLoggedUserIndex)
                {
                    InLoggedUserAccount = item.Value;
                }
            }
        }

        //Creates a admin profile. 
        //Call this method when the program starts.
        public void CreateAdmin()
        {
            Admin admin = new Admin();
            PersonDictionary.Add(PersonDictionary.Count, admin);

            //creates a bankaccount and adds it to the dictionary even though
            //the admin can never access it just to make the keys in the two
            //dictionaries match
            BankAccount newBankAccount = new BankAccount();
            AccountDictionary.Add(AccountDictionary.Count, newBankAccount);
        }

        //Creates a customer profile with input from the admin.
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
        public void ChangeExchageRate()
        {
            Console.Write("\n\tCurrent exchange rate:" +
                $" Euro {SekMultiplierFromEuro} Dollar {SekMultiplierFromDollar}");

            Console.Write("\n\tWhats todays exchage rate for Dollar: ");
            decimal.TryParse(Console.ReadLine(), out decimal RateDollar);
            SekMultiplierFromDollar = RateDollar;

            Console.Write("\n\tWhats todays exchage rate for Euro: ");
            decimal.TryParse(Console.ReadLine(), out decimal RateEuro);
            SekMultiplierFromEuro = RateEuro;
            AdminMenu();
        }

        //Menu for customer users.
        public void CustomerMenu()
        {
            Console.Clear();
            Console.Write("\n\t[1]Add new bank account" +
                "\n\t[2]Show accounts" +
                "\n\t[3]Move money" +
                "\n\t[4]Logout" +
                "\n\t: ");
            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    InLoggedUserAccount.AddNewBankAccount();
                    CustomerMenu();
                    break;
                case 2:
                    InLoggedUserAccount.DisplayAccountInfo();
                    Console.ReadLine();
                    CustomerMenu();
                    break;
                case 3:
                    InLoggedUserAccount.DisplayAccountInfo();
                    InLoggedUserAccount.TransfereMoneyinUser();
                    CustomerMenu();
                    break;
                case 4:
                    Login();
                    break;
                default:
                    CustomerMenu();
                    break;
            }
        }

        //Menu for admin users.
        public void AdminMenu()
        {
            Console.Clear();
            Console.Write("\n\t[1]Create account" +
                "\n\t[2]Change currency rate" +
                "\n\t[3]Logout" +
                "\n\t: ");
            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    CreateUserAccount();
                    break;
                case 2:
                    ChangeExchageRate();
                    break;
                case 3:
                    Login();
                    break;
                default:
                    AdminMenu();
                    break;
            }
        }
    }
}
