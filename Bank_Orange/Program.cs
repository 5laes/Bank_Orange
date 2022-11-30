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
            bankSystem.DefaultUser();
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(10, 10);
            bankSystem.CreateAdmin();
            bankSystem.GetCurrencyExchanges(currencyExchanges);
            bankSystem.Login();
        }
    }
}
