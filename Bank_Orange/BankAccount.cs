using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bank_Orange
{
    class BankAccount
    {
        //A list of account details.
        private List<AccountDetails> BankAccountList = new List<AccountDetails>();
        private decimal SekMultiplierFromEuro = 10.9m;
        private decimal SekMultiplierFromDollar = 10.5m;

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
                return money /= SekMultiplierFromDollar;
            }
            if (currency == "€")
            {
                return money /= SekMultiplierFromEuro;
            }
            return money;
        }

        //Converts currencies when transferen money between accounts
        public decimal CurrencyConverter(string currencyFrom,string currencyTo, decimal money)
        {
            if (currencyFrom == "$" && currencyTo == "€")
            {
                money *= SekMultiplierFromDollar;
                return money /= SekMultiplierFromEuro;
            }
            if (currencyFrom == "€" && currencyTo == "$")
            {
                money *= SekMultiplierFromEuro;
                return money /= SekMultiplierFromDollar;
            }
            if (currencyFrom == "Kr" && currencyTo == "€")
            {
                return money /= SekMultiplierFromEuro;
            }
            if (currencyFrom == "Kr" && currencyTo == "$")
            {
                return money /= SekMultiplierFromDollar;
            }
            if (currencyFrom == "€" && currencyTo == "Kr")
            {
                return money *= SekMultiplierFromEuro;
            }
            if (currencyFrom == "$" && currencyTo == "Kr")
            {
                return money *= SekMultiplierFromDollar;
            }
            return money;
        }

        //Method to transfere money within a useraccount.
        public void TransfereMoneyinUser()
        {
            Console.Write("\n\tWhat account do you want to withdrawl from: ");
            int.TryParse(Console.ReadLine(), out int withdrawl);
            Console.Write("\n\tHow much money do you want to withdrawl: ");
            decimal.TryParse(Console.ReadLine(), out decimal money);
            Console.Write("\n\tWhat account do you want to depossit to: ");
            int.TryParse(Console.ReadLine(), out int depossit);

            AccountDetails withdrawlAccount = BankAccountList.ElementAt(withdrawl - 1);
            AccountDetails depossitAccount = BankAccountList.ElementAt(depossit - 1);
            withdrawlAccount.Money = withdrawlAccount.Money - money;

            money = CurrencyConverter(withdrawlAccount.CurrencyType, depossitAccount.CurrencyType, money);

            depossitAccount.Money = depossitAccount.Money + money;
        }
    }
}
