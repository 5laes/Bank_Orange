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

        private string currencyType;
        public string CurrencyType
        {
            get { return currencyType; }
            set { currencyType = value; }
        }

        private bool currencyPosition;
        public bool CurrencyPosition
        {
            get { return currencyPosition; }
            set { currencyPosition = value; }
        }

        public AccountDetails(string accountName, decimal money, string currency, bool currencyPosition)
        {
            AccountName = accountName;
            Money = money;
            CurrencyType = currency;
            CurrencyPosition = currencyPosition;
        }     

        
    }
}
