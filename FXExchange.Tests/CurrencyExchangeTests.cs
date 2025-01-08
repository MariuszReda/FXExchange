using FXExchange.Domain;
using FXExchange.Tests.Mock;

namespace FXExchange.Tests
{
    public class CurrencyExchangeTests
    {
        [Fact]
        public void GetExchangeEURtoDDK() 
        {
            var rate = ExchangeRateMock.GetExchangeDKK(new Currency("EUR"));

            Assert.Equal(rate, 7.4394m);
        }

        [Fact]
        public void GetExchangeEURtoGBP()
        {
            var rateEUR = Math.Round(ExchangeRateMock.GetExchangeDKK(new Currency("EUR")),4);
            var rateGBP = Math.Round(ExchangeRateMock.GetExchangeDKK(new Currency("GBP")),4);

            var exchangeRateEURtoGBP = Math.Round(rateEUR / rateGBP, 4);

            Assert.Equal(0.8723m, exchangeRateEURtoGBP);
        }

        [Fact]
        public void GetExchangeDKK_InvalidIsoCode_ShouldThrowException()
        {
            var mock = ExchangeRateMock.GetRatesCurrencies();

            var invalidIsoCode = "XYZ";

            List<Currency> listCurreny = mock.Keys.ToList();

            var exception = Assert.Throws<ArgumentException>(() => Currency.GetCurrency(invalidIsoCode, listCurreny));

            Assert.Equal("Currency with ISO code 'XYZ' not found.", exception.Message);
        }

    }
}