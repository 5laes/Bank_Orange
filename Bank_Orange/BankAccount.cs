using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;

namespace Bank_Orange
{
    public class BankAccount
    {
        //For saving the exchangerate
        public CurrencyExchanges currencyExchanges;

        public bool CanTakeLoan = true;

        //A list of account details.
        public List<AccountDetails> BankAccountList = new List<AccountDetails>();

        public List<string> LogList = new List<string>();

        //Displays all the regular accounts that a user has and the information in them.
        public void DisplayAccountInfo()
        {
            Console.Clear();
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;

            foreach (var AccountDetails in BankAccountList)
            {
                if (AccountDetails.IsSavingsAccount == false)
                {
                    if (AccountDetails.CurrencyPosition == false)
                    {
                        Console.Write($"\n\t[{AccountDetails.AccountIndex + 1}]{AccountDetails.AccountName}: " +
                            $"{AccountDetails.CurrencyType}{CurrencyFormat(AccountDetails.Money)} ");
                    }
                    else
                    {
                        Console.Write($"\n\t[{AccountDetails.AccountIndex + 1}]{AccountDetails.AccountName}: " +
                            $"{CurrencyFormat(AccountDetails.Money)}{AccountDetails.CurrencyType} ");
                    }
                }
            }
        }

        //Displays all the savings accounts that a user has and the information in them.
        public void DisplaySavingsInfo()
        {
            Console.Clear();
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;

            foreach (var AccountDetails in BankAccountList)
            {
                if (AccountDetails.IsSavingsAccount == true)
                {
                    if (AccountDetails.CurrencyPosition == false)
                    {
                        Console.Write($"\n\t[{AccountDetails.AccountIndex + 1}]{AccountDetails.AccountName}: " +
                            $"{AccountDetails.CurrencyType}{CurrencyFormat(AccountDetails.Money)} " +
                            $"\n\tWith 8% interest, in 5 years you will have: {AccountDetails.Money * 1.08m * 1.08m * 1.08m * 1.08m * 1.08m:0.00}\n");
                    }
                    else
                    {
                        Console.Write($"\n\t[{AccountDetails.AccountIndex + 1}]{AccountDetails.AccountName}: " +
                            $"{CurrencyFormat(AccountDetails.Money)}{AccountDetails.CurrencyType} " +
                            $"\n\tWith 8% interest, in 5 years you will have: {AccountDetails.Money * 1.08m * 1.08m * 1.08m * 1.08m * 1.08m:0.00}\n");
                    }
                }

               
            }
        }

        //Displays all the accounts that a user has and the information in them.
        public void DisplayAllAccountInfo()
        {
            Console.Clear();

            foreach (var AccountDetails in BankAccountList)
            {
                    if (AccountDetails.CurrencyPosition == false)
                    {
                    Console.Write($"\n\t[{AccountDetails.AccountIndex + 1}]{AccountDetails.AccountName}: " +
                        $"{AccountDetails.CurrencyType}{CurrencyFormat(AccountDetails.Money)}");
                    }
                    else
                    {
                    Console.Write($"\n\t[{AccountDetails.AccountIndex + 1}]{AccountDetails.AccountName}: " +
                        $"{CurrencyFormat(AccountDetails.Money)}{AccountDetails.CurrencyType}");
                    }
            }
        }

        //Gets the current currencyExchange
        public void GetCurrencyExchanges(CurrencyExchanges newCurrencyExchanges)
        {
            currencyExchanges = newCurrencyExchanges;
        }

        //Creates a new account with specific name and currency.
        public void AddNewBankAccount()
        {
            //Console.Clear();
            bool currencyPosition;

            Console.Write($"\n\tDo you want this to be an savings account or deposit account?" +
                "\n\t[1] Deposit account" +
                "\n\t[2] Savings account - Current interest rate is: 8%" +
                "\n\t: ");
            
            int.TryParse(Console.ReadLine(), out int choice);

            bool isSavingsAccount;
            switch (choice)
            {
                case 1:
                    isSavingsAccount = false;
                    break;

                case 2:
                    isSavingsAccount = true;
                    break;

                default:
                    isSavingsAccount = false;
                    break;
            }

            
            Console.Write("\n\tName account: ");
            string accountName = Console.ReadLine();

            Console.Write("\n\tPick a currency " +
                "\n\t[1] Sek" +
                "\n\t[2] Dollar" +
                "\n\t[3] Euro" +
                "\n\t: ");
            int.TryParse(Console.ReadLine(), out int pick);
            string currency;
            switch (pick)
            {
                case 1:
                    currency = "Kr";
                    currencyPosition = true;
                break;
                case 2:
                    currency = "$";
                    currencyPosition = false;
                    break;
                case 3:
                    currency = "€";
                    currencyPosition = false;
                    break;
                default:
                    currency = "Kr";
                    currencyPosition = true;
                    break;
            }

            Console.Write("\n\tMoney to deposit in sek: ");
            decimal.TryParse(Console.ReadLine(), out decimal money);

            money = CurrencyConvertFromSek(currency, money);
            int accountIndex = BankAccountList.Count;
            
            AccountDetails newAccount = new AccountDetails(accountName, money, currency, currencyPosition, isSavingsAccount, accountIndex);
            BankAccountList.Add(newAccount);

            string Log = $"{DateTime.Now}: You created an account named {accountName} and deposited {CurrencyFormat(money)} in {currency}";
            LogList.Add(Log);
        }

        //Converts currencies from sek to dollar/euro when a account is created.
        public decimal CurrencyConvertFromSek(string currency, decimal money)
        {
            if (currency == "$")
            {
                return money /= currencyExchanges.DollarCurrencyRate;
            }
            if (currency == "€")
            {
                return money /= currencyExchanges.EuroCurrencyRate;
            }
            return money;
        }

        //Converts currencies when transferen money between accounts
        public decimal CurrencyConverter(string currencyFrom,string currencyTo, decimal money)
        {
            if (currencyFrom == "$" && currencyTo == "€")
            {
                money *= currencyExchanges.DollarCurrencyRate;
                return money /= currencyExchanges.EuroCurrencyRate;
            }
            if (currencyFrom == "€" && currencyTo == "$")
            {
                money *= currencyExchanges.EuroCurrencyRate;
                return money /= currencyExchanges.DollarCurrencyRate;
            }
            if (currencyFrom == "Kr" && currencyTo == "€")
            {
                return money /= currencyExchanges.EuroCurrencyRate;
            }
            if (currencyFrom == "Kr" && currencyTo == "$")
            {
                return money /= currencyExchanges.DollarCurrencyRate;
            }
            if (currencyFrom == "€" && currencyTo == "Kr")
            {
                return money *= currencyExchanges.EuroCurrencyRate;
            }
            if (currencyFrom == "$" && currencyTo == "Kr")
            {
                return money *= currencyExchanges.DollarCurrencyRate;
            }
            return money;
        }

        //Method to transfere money within a useraccount.
        public void TransfereMoneyinUser()
        {
            Console.Write("\n\tWhat account do you want to transfer from: ");
            int.TryParse(Console.ReadLine(), out int withdrawl);

            Console.Write("\n\tHow much money do you want to transfer: ");
            decimal.TryParse(Console.ReadLine(), out decimal money);

            Console.Write("\n\tWhat account do you want to depossit to: ");
            int.TryParse(Console.ReadLine(), out int depossit);
            
            AccountDetails withdrawlAccount;
            AccountDetails depossitAccount;

            try
            {
                withdrawlAccount = BankAccountList.ElementAt(withdrawl - 1);
                depossitAccount = BankAccountList.ElementAt(depossit - 1);
             
                if (money == 0)
                {
                    Console.Write("\n\tPlease enter a valid number.");
                    Console.ReadLine();
                }
                else if(money < 0)
                {
                    money = 0;
                    
                    Console.Write("\n\tCan not send a negative amount.");
                    Console.ReadLine();
                }
                else if (money <= withdrawlAccount.Money)
                {
                    withdrawlAccount.Money -= money;
                    //money = CurrencyConverter(withdrawlAccount.CurrencyType, depossitAccount.CurrencyType, money);
                    depossitAccount.Money += CurrencyConverter(withdrawlAccount.CurrencyType, depossitAccount.CurrencyType, money);

                    Console.Write($"\n\tTransaction has been successful.");

                    string log = $"{DateTime.Now}: You transfered {CurrencyFormat(money)} in {withdrawlAccount.CurrencyType} from {withdrawlAccount.AccountName} " +
                        $"to {depossitAccount.AccountName}";
                    LogList.Add(log);
                    Console.ReadLine();
                }
                else
                {
                    Console.Write($"\n\tInsufficent funds.");
                    Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.Write($"\n\tOne of the accounts you are trying to access does not exist.");
                Console.ReadKey();
            }
        }

        //Method that return the ammount of money a user wants to send to another user
        public decimal SendMoney(int receiverID)
        {
            Console.Write("\n\tWhat account do you want to send from: ");
            int.TryParse(Console.ReadLine(), out int send);

            Console.Write("\n\tHow much money do you want to send: ");
            decimal.TryParse(Console.ReadLine(), out decimal money);

            AccountDetails sendAccount;

            try
            {
                sendAccount = BankAccountList.ElementAt(send - 1);

                if (money == 0)
                {
                    Console.Write("\n\tPlease enter a valid number.");
                    Console.ReadLine();
                }
                else if (money < 0)
                {
                    money = 0;

                    Console.Write("\n\tCan not send a negative amount.");
                    Console.ReadLine();
                }
                else if (money <= sendAccount.Money)
                {
                    sendAccount.Money -= money;
                    money = CurrencyConverter(sendAccount.CurrencyType, "Kr", money);
                   
                    Console.Write($"\n\tTransaction has been successful.");

                    string log = $"{DateTime.Now}: You sent {CurrencyFormat(money)} in {sendAccount.CurrencyType} to a bank account with the bank ID {receiverID} at";
                    LogList.Add(log);
                    Console.ReadKey();
                }
                else
                {
                    Console.Write($"\n\tInsufficent funds.");
                    Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.Write($"\n\tOne of the accounts you are trying to access does not exist.");
                Console.ReadKey();
            }
            return money;
        }

        //Method that recieves money from another user
        public void RecievMoney(decimal money, int senderID)
        {
            try
            {
                AccountDetails recievAccount = BankAccountList.ElementAt(0);
                money = CurrencyConverter("Kr", recievAccount.CurrencyType, money);
                recievAccount.Money += money;

                string log;
                if (senderID == -1)
                {
                    log = $"{DateTime.Now}: You made a loan of {CurrencyFormat(money)} in {recievAccount.CurrencyType}";
                    LogList.Add(log); 
                }
                else
                {
                    log = $"{DateTime.Now}: You received {CurrencyFormat(money)} in {recievAccount.CurrencyType} from bank ID {senderID}";
                    LogList.Add(log);
                }
            }
            catch (Exception)
            {
                Console.Write($"\n\tThe recipent does not have an bankaccount.");
                Console.ReadKey();
            }
        }

        //for testing
        public void AddTestAccounts()
        {
            AccountDetails testAcc1 = new AccountDetails("A", 10000, "Kr", true, false, BankAccountList.Count);
            BankAccountList.Add(testAcc1);

            AccountDetails testAcc2 = new AccountDetails("B", 1000, "$", false, false, BankAccountList.Count);
            BankAccountList.Add(testAcc2);

            AccountDetails testAcc3 = new AccountDetails("C", 1000, "€", false, false, BankAccountList.Count);
            BankAccountList.Add(testAcc3);

            AccountDetails testAcc4 = new AccountDetails("D", 10000, "Kr", true, true, BankAccountList.Count);
            BankAccountList.Add(testAcc4);
        }

        //Gets the total ammount of money a users has on all the accounts
        public decimal TotalMoney()
        {
            decimal totalMoney = 0;
            foreach (var item in BankAccountList)
            {
                decimal money = CurrencyConverter(item.CurrencyType, "Kr", item.Money);
                totalMoney = totalMoney + money;
            }
            return totalMoney;
        }

        public void AccountCreationLog()
        {
            string creation = $"{DateTime.Now}: Your account was created";
            LogList.Add(creation);
        }

        public void DisplayLogHistory()
        {
            //Console.Clear();
            foreach (var item in LogList)
            {
                Console.Write($"\n\t{item}.");
            }
            Console.ReadLine();
        }
        public static string CurrencyFormat(decimal d)
        {
            string test = d.ToString("C");

            return test.Trim('k','r', ' ');
        }
    }
}
