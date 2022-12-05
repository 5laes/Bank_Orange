using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;

namespace Bank_Orange
{
    class BankAccount
    {
        //For saving the exchangerate
        CurrencyExchanges currencyExchanges;

        //A list of account details.
        private List<AccountDetails> BankAccountList = new List<AccountDetails>();

        //Displays all the accounts that a user has and the information in them.
        public void DisplayAccountInfo()
        {
            Console.Clear();
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;
            int accountIndex = 1;
            Console.Clear();
            foreach (var AccountDetails in BankAccountList)
            {
                if (AccountDetails.CurrencyPosition == false)
                {
                    Console.Write($"\n\t[{accountIndex}]{AccountDetails.AccountName}: " +
                        $"{AccountDetails.CurrencyType}{AccountDetails.Money:0.00} ");
                }
                else 
                {
                    Console.Write($"\n\t[{accountIndex}]{AccountDetails.AccountName}: " +
                        $"{AccountDetails.Money:0.00}{AccountDetails.CurrencyType} ");
                }
                accountIndex++;
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
            Console.Clear();
            bool currencyPosition;

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

            AccountDetails newAccount = new AccountDetails(accountName, money, currency, currencyPosition);
            BankAccountList.Add(newAccount);
        }

        //Converts currencies from sek to dollar/euro when a account is created.
        public decimal CurrencyConvertFromSek(string currency, decimal money)
        {
            if (currency == "$")
            {
                return money /= currencyExchanges.SekMultiplierFromDollar;
            }
            if (currency == "€")
            {
                return money /= currencyExchanges.SekMultiplierFromEuro;
            }
            return money;
        }

        //Converts currencies when transferen money between accounts
        public decimal CurrencyConverter(string currencyFrom,string currencyTo, decimal money)
        {
            if (currencyFrom == "$" && currencyTo == "€")
            {
                money *= currencyExchanges.SekMultiplierFromDollar;
                return money /= currencyExchanges.SekMultiplierFromEuro;
            }
            if (currencyFrom == "€" && currencyTo == "$")
            {
                money *= currencyExchanges.SekMultiplierFromEuro;
                return money /= currencyExchanges.SekMultiplierFromDollar;
            }
            if (currencyFrom == "Kr" && currencyTo == "€")
            {
                return money /= currencyExchanges.SekMultiplierFromEuro;
            }
            if (currencyFrom == "Kr" && currencyTo == "$")
            {
                return money /= currencyExchanges.SekMultiplierFromDollar;
            }
            if (currencyFrom == "€" && currencyTo == "Kr")
            {
                return money *= currencyExchanges.SekMultiplierFromEuro;
            }
            if (currencyFrom == "$" && currencyTo == "Kr")
            {
                return money *= currencyExchanges.SekMultiplierFromDollar;
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
                    money = CurrencyConverter(withdrawlAccount.CurrencyType, depossitAccount.CurrencyType, money);
                    depossitAccount.Money += money;

                    Console.Write($"\n\tTransaction has been successful.");
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
        public decimal SendMoney()
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
        public void RecievMoney(decimal money)
        {
            try
            {
                AccountDetails recievAccount = BankAccountList.ElementAt(0);
                money = CurrencyConverter("Kr", recievAccount.CurrencyType, money);
                recievAccount.Money += money;
            }
            catch (Exception)
            {
                Console.Write($"\n\tThe recipent does not have an bankaccount.");
                Console.ReadKey();
            }
        }
    }
}
