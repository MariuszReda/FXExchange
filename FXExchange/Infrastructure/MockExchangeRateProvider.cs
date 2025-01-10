using FXExchange.Application.Interfaces;
using FXExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Infrastructure
{
    public class MockExchangeRateProvider : IExchangeRateProvider
    {

        private static readonly Dictionary<string, decimal> _rates = new()
        {
            { "EUR", 7.4394m },
            { "USD", 6.6311m },
            { "GBP", 8.5285m },
            { "SEK", 0.7610m },
            { "NOK", 0.7840m },
            { "CHF", 6.8358m },
            { "JPY", 0.059740m },
            { "DKK", 1m }
        };
        public Dictionary<string, decimal> GetRates()
        {
            return _rates;
        }
        public bool IsValidIsoCode(string isoCode)
        {
            return _rates.ContainsKey(isoCode);
        }

        public decimal GetExchangeRate(Currency source, Currency target)
        {
            if (source == null || target == null)
            {
                throw new ArgumentException("Waluty źródłowa i docelowa muszą być podane.");
            }

            if (!_rates.ContainsKey(source.IsoCode) || !_rates.ContainsKey(target.IsoCode))
            {
                throw new ArgumentException("Nieznana waluta.");
            }

            if (source.GetHashCode() == target.GetHashCode())
            {
                return 1m;
            }

            decimal sourceToDKK = _rates[source.IsoCode];
            decimal targetToDKK = _rates[target.IsoCode];

            return targetToDKK / sourceToDKK;
        }

    }

}
