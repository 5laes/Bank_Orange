using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    internal class PendingTransactions
    {
        public BankAccount Receiver;
        public decimal Money;

        public PendingTransactions(BankAccount receiver, decimal money)
        {
            Receiver = receiver;
            Money = money;
        }
    }
}
