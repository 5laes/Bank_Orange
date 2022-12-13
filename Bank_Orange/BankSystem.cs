using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Bank_Orange
{
    class BankSystem
    {
        //for saving the transactions
        PendingTransactions pendingTransactions;

        //A queue with transactions
        Queue<PendingTransactions> pendingTransactionsQueue = new Queue<PendingTransactions>(); 

        //For saving the exchangerate
        CurrencyExchanges currencyExchanges;

        //For saving the users index.
        int InLoggedUserIndex;

        //For saving the users account.
        BankAccount InLoggedUserAccount;

        //Dictionaries of users and users accounts where the key keeps them together.
        private Dictionary<int, Person> PersonDictionary = new Dictionary<int, Person>();
        private Dictionary<int, BankAccount> AccountDictionary = new Dictionary<int, BankAccount>();

        //method that repeteadly checks calls EmptyQueue so that the queue will empty at the correct time
        public void CheckTime()
        {
            while (true)
            {
                EmptyQueue();
                Thread.Sleep(500);
            }
        }

        //Gets the current currencyExchange
        public void GetCurrencyExchanges(CurrencyExchanges newCurrencyExchanges)
        {
            currencyExchanges = newCurrencyExchanges;
        }

        //Login method thats saves the users index if a loggin is successfull.
        public void Login()
        {
            for (int Attempts = 0; Attempts <= 3; Attempts++)
            {
                if (Attempts < 3)
                {
                    Console.Clear();
                    Console.Write($"" +
                        $"\n\t,-----.                  ,--.         ,-----.                                       " +
                        $"\n\t|  |) /_  ,--,--.,--,--, |  |,-.     '  .-.  ',--.--. ,--,--.,--,--,  ,---.  ,---.  " +
                        $"\n\t|  .-.  \\' ,-.  ||      \\|     /     |  | |  ||  .--'' ,-.  ||      \\| .-. || .-. : " +
                        $"\n\t|  '--' /\\ '-'  ||  ||  ||  \\  \\     '  '-'  '|  |   \\ '-'  ||  ||  |' '-' '\\   --. " +
                        $"\n\t`------'  `--`--'`--''--'`--'`--'     `-----' `--'    `--`--'`--''--'.`-  /  `----' " +
                        $"\n\t                                                                     `---'          " +
                        $"\n\t______________________________________________________________________________________" +
                        $"\n\t");
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
            newBankAccount.GetCurrencyExchanges(currencyExchanges);
            newBankAccount.AccountCreationLog();
            AccountDictionary.Add(AccountDictionary.Count, newBankAccount);

            AdminMenu();
        }

        //Method for admin to uppdate the exchange rate of dollar and euro
        public void ChangeExchageRate()
        {
            Console.Write("\n\tCurrent exchange rate: " +
                $"Euro {currencyExchanges.EuroCurrencyRate} Dollar {currencyExchanges.DollarCurrencyRate}");

            Console.Write("\n\tWhats todays exchage rate for Dollar: ");
            decimal.TryParse(Console.ReadLine(), out decimal RateDollar);
            currencyExchanges.DollarCurrencyRate = RateDollar;

            Console.Write("\n\tWhats todays exchage rate for Euro: ");
            decimal.TryParse(Console.ReadLine(), out decimal RateEuro);
            currencyExchanges.EuroCurrencyRate = RateEuro;
            AdminMenu();
        }

        //Menu for customer users.
        public void CustomerMenu()
        {
            Console.Clear();
            Console.Write($"" +
                $"\n\t   ______________________________" +
                $"\n\t / \\                             \\." +
                $"\n\t|   | [1]Add new bank account    |." +
                $"\n\t \\_ | [2]Show accounts           |." +
                $"\n\t    | [3]Show savings            |." +
                $"\n\t    | [4]Move money              |." +
                $"\n\t    | [5]Send money              |." +
                $"\n\t    | [6]Borrow money            |." +
                $"\n\t    | [7]Show Log                |." +
                $"\n\t    | [8]Log out                 |." +
                $"\n\t    |                            |." +
                $"\n\t    | Your account ID is {InLoggedUserIndex}       |." +
                $"\n\t    |                            |." +
                $"\n\t    |                            |." +
                $"\n\t    |                            |." +
                $"\n\t    |   _________________________|___" +
                $"\n\t    |  /                            /." +
                $"\n\t    \\_/BO__________________________/." +
                $"\n\n\b\t\t:");

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
                    InLoggedUserAccount.DisplaySavingsInfo();
                    Console.ReadLine();
                    CustomerMenu();
                    break;
                case 4:
                    InLoggedUserAccount.DisplayAllAccountInfo();
                    InLoggedUserAccount.TransfereMoneyinUser();
                    CustomerMenu();
                    break;
                case 5:
                    TransfereBetweenUsers();
                    break;
                case 6:
                    if (InLoggedUserAccount.CanTakeLoan == true)
                    {
                        BorrowMoney();
                    }
                    else
                    {
                        Console.Write("\n\tYou can only have max 1 loan! You currently have 1 loan.");
                        Console.ReadLine();
                    }
                    break;
                case 7:
                    InLoggedUserAccount.DisplayLogHistory();
                    CustomerMenu();
                    break;
                case 8:
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
            //Console.Write("\n\t[1]Create account" +
            //    "\n\t[2]Change currency rate" +
            //    "\n\t[3]Logout" +
            //    "\n\t: ");
            Console.Write($"\t             ____________________________________________________" +
                $"\n\t            /                                                    \\" +
                $"\n\t           |    _____________________________________________     |" +
                $"\n\t           |   |                                             |    |" +
                $"\n\t           |   |  C:\\secret\\admin_settings> _                |    |" +
                $"\n\t           |   |                                             |    |" +
                $"\n\t           |   |  [1]Create account                          |    |" +
                $"\n\t           |   |  [2]Change currency rate                    |    |" +
                $"\n\t           |   |  [3]Logout                                  |    |" +
                $"\n\t           |   |                                             |    |" +
                $"\n\t           |   |                                             |    |" +
                $"\n\t           |   |                                             |    |" +
                $"\n\t           |   |                                             |    |" +
                $"\n\t           |   |                                             |    |" +
                $"\n\t           |   |                                             |    |" +
                $"\n\t           |   |_____________________________________________|    |" +
                $"\n\t           |                        Atari                         |" +
                $"\n\t            \\_____________________________________________________/" +
                $"\n\t                   \\_______________________________________/" +
                $"\n\t                _______________________________________________" +
                $"\n\t             _-'    .-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.  --- `-" +
                $"\n\t          _-'.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.--.  .-.-.`-_" +
                $"\n\t       _-'.-.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-`__`. .-.-.-.`-_" +
                $"\n\t    _-'.-.-.-.-. .-----.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-----. .-.-.-.-.`-" +
                $"\n\t    _-'.-.-.-.-. .-----.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-----. .-.-.-.-.`-_" +
                $"\n\t:-----------------------------------------------------------------------------:" +
                $"\n\t`---._.-----------------------------------------------------------------._.---'" +
                $"\n\t\t\t:");

            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    CreateUserAccount();
                    break;
                case 2:
                    Console.Clear();
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

        //Method to transfer money between users.
        public void TransfereBetweenUsers()
        {
            BankAccount receiver;
            Console.Write("\n\tInput recipient ID: ");
            int.TryParse(Console.ReadLine(), out int idKey);
            InLoggedUserAccount.DisplayAccountInfo();
            decimal money = InLoggedUserAccount.SendMoney(idKey);

            foreach (KeyValuePair<int, BankAccount> item in AccountDictionary)
            {
                if (idKey == item.Key)
                {
                    receiver = item.Value;
                    pendingTransactions = new PendingTransactions(receiver, money, InLoggedUserIndex);
                    pendingTransactionsQueue.Enqueue(pendingTransactions);
                }
            }
            CustomerMenu();
        }

        //For testing the program.
        //Dummy users.
        public void LoadTestUsers()
        {
            Customer testUser1 = new Customer("a", "a");
            BankAccount testAcc1 = new BankAccount();

            PersonDictionary.Add(PersonDictionary.Count, testUser1);
            AccountDictionary.Add(AccountDictionary.Count, testAcc1);
            testAcc1.GetCurrencyExchanges(currencyExchanges);

            Customer testUser2 = new Customer("b", "b");
            BankAccount testAcc2 = new BankAccount();

            PersonDictionary.Add(PersonDictionary.Count, testUser2);
            AccountDictionary.Add(AccountDictionary.Count, testAcc2);
            testAcc2.GetCurrencyExchanges(currencyExchanges);

            testAcc1.AddTestAccounts();
            testAcc2.AddTestAccounts();
        }

        //A user can only borrow from the bank once and a maximum of 5x their money
        public void BorrowMoney()
        {
            Console.Clear();
            Console.Write("\n\tHow much money would you like to borrow?" +
                $"\n\tMax amount to borrow is {BankAccount.CurrencyFormat(InLoggedUserAccount.TotalMoney() * 5)} Kr" +
                $"\n\t: ");
            decimal.TryParse(Console.ReadLine(), out decimal borrow);
            if (borrow <= 0)
            {
                Console.Write("\n\tSomething went wrong, please try again.");
            }
            else if (borrow <= InLoggedUserAccount.TotalMoney() * 5)
            {
                Console.Write($"\n\tYou are eligible for this loan.\n\tYour yearly paid interest will be {BankAccount.CurrencyFormat(borrow * 0.035m)}kr!");
                Console.Write("\n\n\tWould you like to complete the transfer?");
                Console.Write("\n\t[1] Yes\n\t[2] No" +
                    "\n\t: ");
                int.TryParse(Console.ReadLine(), out int userChoice);
                if(userChoice == 1)
                {
                    InLoggedUserAccount.CanTakeLoan = false;
                    InLoggedUserAccount.RecievMoney(borrow, -1);
                    Console.Write("\n\tTransfer Complete!");
                }
                else
                {
                    Console.Write("\n\tTransfer was cancelled!");
                }
            }
            else
            {
                Console.Write("\n\tYou are not eligible for this loan!");
            }
            Console.ReadLine();
            CustomerMenu();
        }

        //method to empty the queue of transactions between users
        public void EmptyQueue()
        {
            {
                if (DateTime.Now.Second == 00)
                {
                    while (pendingTransactionsQueue.Count > 0)
                    {
                        PendingTransactions ongoingTransactions = pendingTransactionsQueue.Dequeue();
                        ongoingTransactions.Receiver.RecievMoney(ongoingTransactions.Money, ongoingTransactions.SenderID);
                    }
                }
            }
        }
    }
}
