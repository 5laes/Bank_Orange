using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    class Admin : Person
    {
        public Admin()
        {
            Name = "ADMIN";
            Password = "ADMIN";
            IsAdmin = true;
        }
    }
}
