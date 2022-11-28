using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    class AccountDetails
    {
        private string accountName;
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        private decimal money;
        public decimal Money
        {
            get { return money; }
            set { money = value; }
        }

        public AccountDetails(string accountName, decimal money)
        {
            AccountName = accountName;
            Money = money;
        }     
    }
}
