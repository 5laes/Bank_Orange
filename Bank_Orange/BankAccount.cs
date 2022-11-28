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
        }
    }
}
