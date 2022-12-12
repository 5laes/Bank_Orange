using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    //Class that saves what user and how much money they will get
    internal class PendingTransactions
    {
        public BankAccount Receiver;
        public decimal Money;
        public int SenderID;

        public PendingTransactions(BankAccount receiver, decimal money, int senderID)
        {
            Receiver = receiver;
            Money = money;
            SenderID = senderID;
        }
    }
}
