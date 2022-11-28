using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    class Person
    {
        private string name;
        public string Name 
        { 
            get { return name; } 
            set { name = value; }
        }

        private string password;
        public string Password 
        { 
            get { return password; }
            set { password = value; }
        }

        private bool isAdmin;
        public bool IsAdmin 
        { 
            get { return isAdmin; }
            set { isAdmin = value; }
        }
    }
}
