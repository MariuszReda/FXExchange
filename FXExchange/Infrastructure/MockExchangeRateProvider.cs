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

        private static readonly Dictionary<string, decimal> Rates = new()
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
            return Rates;
        }
        public bool IsValidIsoCode(string isoCode)
        {
            return Rates.ContainsKey(isoCode);
        }

        public decimal GetExchangeRate(Currency currency)
        {
            // Pobranie hashCode z podanego obiektu Currency
            var hashCode = currency.GetHashCode();

            // Przeszukanie słownika w celu dopasowania hashCode
            foreach (var pair in Rates)
            {
                if (pair.Key.GetHashCode() == hashCode)
                {
                    return pair.Value; // Zwrócenie wartości odpowiadającej kluczowi
                }
            }

            // Jeśli nie znaleziono odpowiedniej wartości
            throw new KeyNotFoundException($"No exchange rate found for currency with hashCode {hashCode}.");
        }
    }
    
}
