using FXExchange.Domain;
using FXExchange.Tests.Mock;

namespace FXExchange.Tests
{
    public class CurrencyExchangeTests
    {
        private static Dictionary<Currency, decimal> _mock = ExchangeRateMock.GetRatesCurrencies();
        private static List<Currency> _listCurreny = _mock.Keys.ToList();

        [Fact]
        public void Convert_EURtoDKK_ShouldReturnConvertedAmount() 
        {
            var rate = ExchangeRateMock.GetExchangeDKK(new Currency("EUR"));

            Assert.Equal(rate, 7.4394m);
        }

        [Fact]
        public void Convert_EURtoGBP_ShouldReturnConvertedAmount()
        {
            var rateEUR = Math.Round(ExchangeRateMock.GetExchangeDKK(new Currency("EUR")),4);
            var rateGBP = Math.Round(ExchangeRateMock.GetExchangeDKK(new Currency("GBP")),4);

            var exchangeRateEURtoGBP = Math.Round(rateEUR / rateGBP, 4);

            Assert.Equal(0.8723m, exchangeRateEURtoGBP);
        }

        [Fact]
        public void GetExchangeDKK_InvalidIsoCode_ShouldThrowException()
        {
            var invalidIsoCode = "XYZ";

            var exception = Assert.Throws<ArgumentException>(() => Currency.GetCurrency(invalidIsoCode, _listCurreny));

            Assert.Equal("Currency with ISO code 'XYZ' not found.", exception.Message);
        }

        [Fact]
        public void Convert_SameCurrency_ShouldReturnSameAmount()
        {
            var currency = new Currency("EUR");

            var pair = new CurrencyPair(currency, currency, 1m, _listCurreny);

            var converter = new CurrencyConverter();
            var result = converter.Convert(100, pair);

            Assert.Equal(100m, result);
        }

        [Fact]
        public void Convert_UnknownCurrency_ShouldThrowException()
        {
            var invalidIsoCode = new Currency("XYZ");
            var validCurrency = new Currency("EUR");


            var pair = new CurrencyPair(validCurrency, invalidIsoCode, 1m, _listCurreny);

            var converter = new CurrencyConverter();

            var exception = Assert.Throws<ArgumentException>(() => converter.Convert(100, pair));
            Assert.Equal("Currency with ISO code 'XYZ' not found.", exception.Message);
        }


        [Fact]
        public void Convert_EURtoUSD_ShouldReturnCorrectAmount()
        {
            // Arrange
            var currencyEUR = new Currency("EUR");
            var currencyUSD = new Currency("USD");

            var rateEURtoDKK = ExchangeRateMock.GetExchangeDKK(currencyEUR);
            var rateUSDtoDKK = ExchangeRateMock.GetExchangeDKK(currencyUSD);

            var amount = 100m; // Przyk³adowa kwota w EUR

            // Act
            var result = Math.Round((amount * rateEURtoDKK) / rateUSDtoDKK, 4);

            // Assert
            Assert.Equal(1.1218m, result); // Oczekiwany kurs EUR ? USD przy obecnych danych
        }

    }
}