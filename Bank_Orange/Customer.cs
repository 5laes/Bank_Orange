using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    //Class for creating a customer.
    class Customer : Person
    {
        public Customer(string name, string password)
        {
            UserName = name;
            Password = password;
            IsAdmin = false;
        }
    }
}
