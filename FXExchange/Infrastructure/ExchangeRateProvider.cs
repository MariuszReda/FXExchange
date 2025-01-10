using FXExchange.Application.Interfaces;
using FXExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FXExchange.Infrastructure
{
    public class ExchangeRateProvider : IExchangeRateProvider
    {
        private static Dictionary<string, decimal> _rates;
        private readonly HttpClient _httpClient;
        public ExchangeRateProvider(HttpClient httpClient)
        {
            //if (_rates == null) 
            //{
            //    _rates =  FetchRatesFromApi();
            //}
            _httpClient = httpClient;
        }



        public Dictionary<string, decimal> GetRates()
        {
            throw new NotImplementedException();
        }

        public bool IsValidIsoCode(string isoCode)
        {
            return _rates.ContainsKey(isoCode);
        }

        public  Dictionary<string, decimal> FetchRatesFromApi()
        {
            string Url = "";

            try
            {
                var response = _httpClient.GetAsync(Url).Result;
                response.EnsureSuccessStatusCode();

                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                // string ratesSection = ExtractRatesSection(jsonResponse);

                return  new Dictionary<string, decimal>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching rates from API: {ex.Message}");
                return new Dictionary<string, decimal>();
            }
        }

        public decimal GetExchangeRate(Currency source, Currency target)
        {
            throw new NotImplementedException();
        }
    }
}
