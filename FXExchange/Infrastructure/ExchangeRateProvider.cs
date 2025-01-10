using FXExchange.Application.Interfaces;
using FXExchange.Domain;

namespace FXExchange.Infrastructure
{
    public class ExchangeRateProvider : IExchangeRateProvider
    {
        private static Dictionary<string, decimal> _rates;
        private readonly HttpClient _httpClient;
        public ExchangeRateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task InitializeAsync()
        {
            if (_rates != null && _rates.Count > 0)
            {
                return;
            }

            _rates = new Dictionary<string, decimal>();

            try
            {
                var response = await _httpClient.GetAsync("");
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                using var jsonDocument = System.Text.Json.JsonDocument.Parse(jsonResponse);
                var conversionRatesElement = jsonDocument.RootElement.GetProperty("conversion_rates");

                foreach (var rate in conversionRatesElement.EnumerateObject())
                {
                    if (!_rates.ContainsKey(rate.Name))
                    {
                        _rates.Add(rate.Name, rate.Value.GetDecimal());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing rates: {ex.Message}");
                throw;
            }
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

            decimal sourceToDKK = _rates[target.IsoCode];
            decimal targetToDKK = _rates[source.IsoCode];

            return targetToDKK / sourceToDKK;
        }
    }
}
