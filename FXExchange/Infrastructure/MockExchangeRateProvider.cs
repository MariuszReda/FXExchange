using FXExchange.Application.Interfaces;
using FXExchange.Domain;

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
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public bool IsValidIsoCode(string isoCode)
        {
            return _rates.ContainsKey(isoCode);
        }

        public decimal GetExchangeRate(Currency source, Currency target)
        {
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
