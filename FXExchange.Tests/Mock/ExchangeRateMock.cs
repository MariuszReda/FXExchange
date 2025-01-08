using FXExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Tests.Mock
{
    public static class ExchangeRateMock
    {
        private static readonly Dictionary<Currency, decimal> Rates = new()
        {
            { new Currency("EUR"), 743.94m },
            { new Currency("USD"), 663.11m },
            { new Currency("GBP"), 852.85m },
            { new Currency("SEK"), 76.10m },
            { new Currency("NOK"), 78.40m },
            { new Currency("CHF"), 683.58m },
            { new Currency("JPY"), 5.9740m },
            { new Currency("DKK"), 1 }
        };

        public static Dictionary<Currency, decimal> GetRatesCurrencies()
        {
            return Rates;
        }

        //get exchange per unit
        public static decimal GetExchangeDKK(Currency currency)
        {
            if (!Rates.ContainsKey(currency))
                throw new ArgumentException($"Currency {currency} not found.");
            return Rates[currency] / 100;
        }


    }
}
