using System;
using System.Threading;

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
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(10, 10);
            Thread thread = new Thread(new ThreadStart(bankSystem.CheckTime));
            bankSystem.CreateAdmin();
            thread.Start();
            bankSystem.GetCurrencyExchanges(currencyExchanges);
            bankSystem.LoadTestUsers();
            bankSystem.Login();
        }
    }
}
