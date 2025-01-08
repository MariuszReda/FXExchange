using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Domain
{
    public class CurrencyPair
    {
        public Currency BaseCurrency { get; }
        public Currency QuoteCurrency { get; }
        public decimal ExchangeRate { get; }

        public CurrencyPair(Currency baseCurrency, Currency quoteCurrency, decimal exchangeRate, List<Currency> availableCurrencies)
        {
            if (baseCurrency == null || quoteCurrency == null)
                throw new ArgumentNullException("Currencies cannot be null.");
            if (exchangeRate <= 0)
                throw new ArgumentException("Exchange rate must be positive.");

            BaseCurrency = baseCurrency;
            QuoteCurrency = quoteCurrency;
            ExchangeRate = exchangeRate;

            BaseCurrency = Currency.GetCurrency(baseCurrency.IsoCode, availableCurrencies);

            QuoteCurrency = Currency.GetCurrency(quoteCurrency.IsoCode, availableCurrencies);

            ExchangeRate = exchangeRate;
        }
    }

}
