using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    class Customer : Person
    {
        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            IsAdmin = false;
        }
    }
}
