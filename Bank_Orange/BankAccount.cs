using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bank_Orange
{
    class BankAccount
    {
        private List<AccountDetails> BankAccountList = new List<AccountDetails>();

        public void DisplayAccountInfo()
        {
            int accountIndex = 1;
            Console.Clear();
            foreach (var AccountDetails in BankAccountList)
            {
                Console.Write($"\n\t[{accountIndex}]{AccountDetails.AccountName}: {AccountDetails.Money}kr ");
                accountIndex++;
            }
        }

        public void AddNewBankAccount()
        {
            Console.Write("Name account: ");
            string accountName = Console.ReadLine();
            Console.Write("Money to deposit: ");
            decimal.TryParse(Console.ReadLine(), out decimal money);
            AccountDetails newAccount = new AccountDetails(accountName, money);
            BankAccountList.Add(newAccount);
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
