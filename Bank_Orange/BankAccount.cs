using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    class BankAccount
    {
        private List<AccountDetails> BankAccountList = new List<AccountDetails>();

        public void DisplayAccountInfo()
        {
            Console.Clear();
            foreach (var AccountDetails in BankAccountList)
            {
                Console.WriteLine($"{AccountDetails.AccountName}: {AccountDetails.Money}kr ");
            }
            Console.ReadLine();
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
    }
}
