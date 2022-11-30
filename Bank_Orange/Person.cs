using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    //Base class for any kind of user.
    class Person
    {
        private string userName;
        public string UserName 
        { 
            get { return userName; } 
            set { userName = value; }
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
