using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    class CurrencyExchanges
    {
        public decimal SekMultiplierFromEuro;
        public decimal SekMultiplierFromDollar;

        public CurrencyExchanges(decimal euro, decimal dollar)
        {
            SekMultiplierFromEuro = euro;
            SekMultiplierFromDollar = dollar;
        }
    }
}
