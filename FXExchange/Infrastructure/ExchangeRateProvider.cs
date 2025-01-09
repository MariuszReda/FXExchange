using FXExchange.Application.Interfaces;
using FXExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Infrastructure
{
    public class ExchangeRateProvider : IExchangeRateProvider
    {
        private readonly HttpClient _httpClient;
        public ExchangeRateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public decimal GetExchangeRate(Currency currency)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, decimal> GetRates()
        {
            throw new NotImplementedException();
        }

        public bool IsValidIsoCode(string isoCode)
        {
            throw new NotImplementedException();
        }


    }
}
