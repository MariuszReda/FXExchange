using FXExchange.Domain;
using FXExchange.Infrastructure;

namespace FXExchange.Tests
{
    public class CurrencyExchangeTests
    {
        private static MockExchangeRateProvider mockExchangeRateProvider = new MockExchangeRateProvider();
        private CurrencyFactory factory = new CurrencyFactory(mockExchangeRateProvider);

        [Fact]
        public void GetExchangeEURtoDKK_ShouldReturnConvertedAmount()
        {
            decimal amountInEUR = 100m; 
            Currency sourceCurrency = factory.Create("DKK");          
            Currency targetCurrency = factory.Create("EUR");

            decimal exchangeRate = mockExchangeRateProvider.GetExchangeRate(sourceCurrency, targetCurrency);

            CurrencyPair currencyPair = new CurrencyPair(sourceCurrency, targetCurrency, exchangeRate);

            var result = currencyPair.Convert(amountInEUR);

            Assert.Equal(743.94m, result);
        }


        [Fact]
        public void GetExchangeDKK_InvalidIsoCode_ShouldThrowException()
        {
            var invalidIsoCode = "XYZ";
            var exception = Assert.Throws<KeyNotFoundException>(() =>
            {
                Currency sourceCurrency = factory.Create(invalidIsoCode);
            });

            Assert.Equal("Currency with ISO code 'XYZ' not found.", exception.Message);
        }

        [Fact]
        public void Convert_SameCurrency_ShouldReturnSameAmount()
        {
            Currency currency = factory.Create("EUR");
            decimal exchangeRate = mockExchangeRateProvider.GetExchangeRate(currency, currency);

            var currencyPair = new CurrencyPair(currency, currency, exchangeRate);

            var result = currencyPair.Convert(100);

            Assert.Equal(100m, result);
        }


        [Fact]
        public void GetExchangeDKKtoEUR_ShouldReturnConvertedAmount()
        {
            decimal amountInEUR = 100m;
            Currency sourceCurrency = factory.Create("EUR");
            Currency targetCurrency = factory.Create("DKK");

            decimal exchangeRate = mockExchangeRateProvider.GetExchangeRate(sourceCurrency, targetCurrency);

            CurrencyPair currencyPair = new CurrencyPair(sourceCurrency, targetCurrency, exchangeRate);

            var result = currencyPair.Convert(amountInEUR);

            Assert.Equal(13.4419m, result);
        }


        [Fact]
        public void GetExchangeGBPtoEUR_ShouldReturnConvertedAmount()
        {
            decimal amountInEUR = 100m;
            Currency sourceCurrency = factory.Create("EUR");
            Currency targetCurrency = factory.Create("GBP");

            decimal exchangeRate = mockExchangeRateProvider.GetExchangeRate(sourceCurrency, targetCurrency);

            CurrencyPair currencyPair = new CurrencyPair(sourceCurrency, targetCurrency, exchangeRate);

            var result = currencyPair.Convert(amountInEUR);

            Assert.Equal(114.6396m, result);
        }

    }
}