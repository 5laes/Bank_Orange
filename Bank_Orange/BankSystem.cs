using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    class BankSystem
    {
        int InLoggedUserIndex;

        private Dictionary<int, Person> PersonDictionary = new Dictionary<int, Person>();
        private Dictionary<int, BankAccount> AccountDictionary = new Dictionary<int, BankAccount>();

        public void Login()
        {
            for (int Attempts = 0; Attempts <= 3; Attempts++)
            {
                if (Attempts < 3)
                {
                    
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
