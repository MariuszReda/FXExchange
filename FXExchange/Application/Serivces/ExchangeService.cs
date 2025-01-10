using FXExchange.Application.Interfaces;
using FXExchange.Domain;
using FXExchange.Infrastructure;

namespace FXExchange.Application.Serivces
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangeRateProvider _exchangeRateProvider;

        public ExchangeService(IExchangeRateProvider exchangeRateProvider)
        {
            _exchangeRateProvider = exchangeRateProvider;
        }

        public decimal PerformExchange(string sourceCurrencyCode, string targetCurrencyCode, decimal amount)
        {
            CurrencyFactory factory = new CurrencyFactory(_exchangeRateProvider);
            Currency sourceCurrency = factory.Create(sourceCurrencyCode);
            Currency targetCurrency = factory.Create(targetCurrencyCode);
            decimal exchangeRate = _exchangeRateProvider.GetExchangeRate(sourceCurrency, targetCurrency);
            CurrencyPair currencyPair = new CurrencyPair(sourceCurrency, targetCurrency, exchangeRate);
            return currencyPair.Convert(amount);
        }
    }
}
