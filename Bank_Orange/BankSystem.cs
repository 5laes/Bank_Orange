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
    }
}
