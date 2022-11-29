using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bank_Orange
{
    class BankAccount
    {
        private List<AccountDetails> BankAccountList = new List<AccountDetails>();
        private decimal SekMultiplierFromEuro = 10.9m;
        private decimal SekMultiplierFromDollar = 10.5m;


        public void DisplayAccountInfo()
        {
            Console.InputEncoding = Console.OutputEncoding = System.Text.Encoding.Unicode;
            int accountIndex = 1;
            Console.Clear();
            foreach (var AccountDetails in BankAccountList)
            {
                if (AccountDetails.CurrencyPosition == false)
                {
                    Console.Write($"\n\t[{accountIndex}]{AccountDetails.AccountName}: " +
                        $"{AccountDetails.CurrencyType}{AccountDetails.Money.ToString("0.00")} ");
                }
                else 
                {
                    Console.Write($"\n\t[{accountIndex}]{AccountDetails.AccountName}: " +
                        $"{AccountDetails.Money.ToString("0.00")}{AccountDetails.CurrencyType} ");
                }
                accountIndex++;
            }
        }

        public void AddNewBankAccount()
        {
            bool currencyPosition = true;

            Console.Write("Name account: ");
            string accountName = Console.ReadLine();
            Console.WriteLine("Pick a currency: [1] Sek, [2] Dollar, [3] Euro ");
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

            Console.Write("Money to deposit in sek: ");
            decimal.TryParse(Console.ReadLine(), out decimal money);
            money = CurrencyConvert(currency, money);
            AccountDetails newAccount = new AccountDetails(accountName, money, currency, currencyPosition);
            BankAccountList.Add(newAccount);
        }
        public decimal CurrencyConvert(string currency, decimal money)
        {
            if (currency == "$")
            {
                return money = money / SekMultiplierFromDollar;
            }
            if (currency == "€")
            {
                return money = money / SekMultiplierFromEuro;
            }
            return money;
        }

        public void TransfereMoneyinUser()
        {
            Console.Write("\n\tWhat account do you want to withdrawl from: ");
            int.TryParse(Console.ReadLine(), out int withdrawl);
            Console.Write("\n\tHow much money do you want to withdrawl: ");
            decimal.TryParse(Console.ReadLine(), out decimal money);
            Console.Write("\n\tWhat account do you want to depossit to: ");
            int.TryParse(Console.ReadLine(), out int depossit);

            AccountDetails withdrawlAccount = BankAccountList.ElementAt(withdrawl - 1);
            withdrawlAccount.Money = withdrawlAccount.Money - money;

            AccountDetails depossitAccount = BankAccountList.ElementAt(depossit - 1);
            depossitAccount.Money = depossitAccount.Money + money;
        }
    }
}
