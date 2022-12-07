using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Orange
{
    //Class for saving/editing currency exchanges
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
