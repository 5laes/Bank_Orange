using System;

namespace Bank_Orange
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadProgram();
        }

        public static void LoadProgram()
        {
            BankSystem bankSystem = new BankSystem();
            bankSystem.CreateAdmin();
            bankSystem.Login();
        }
    }
}
