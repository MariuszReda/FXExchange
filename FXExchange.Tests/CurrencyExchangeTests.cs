using FXExchange.Application;
using FXExchange.Domain;
using FXExchange.Infrastructure;
using System;

namespace FXExchange.Tests
{
    public class CurrencyExchangeTests
    {
        private static MockExchangeRateProvider mockExchangeRateProvider = new MockExchangeRateProvider();      

        [Fact]
        public void GetExchangeEURtoDKK_ShouldReturnConvertedAmount()
        {
            var factory = new CurrencyFactory(mockExchangeRateProvider);

            decimal amountInEUR = 10m; // 10 EURO
            Currency sourceCurrency = factory.Create("DKK");          
            Currency targetCurrency = factory.Create("EUR");

            var rates = mockExchangeRateProvider.GetRates();
            decimal exchangeRate = mockExchangeRateProvider.GetExchangeRate(targetCurrency);

            CurrencyPair currencyPair = new CurrencyPair(sourceCurrency, targetCurrency, exchangeRate);

            var result = currencyPair.Convert(amountInEUR);

            Assert.Equal(74.3940m, result);
        }

        //[Fact]
        //public void Convert_EURtoGBP_ShouldReturnConvertedAmount()
        //{


        //    decimal amountInEUR = 1m; // 1 EUR
        //    Currency sourceCurrency = new Currency("EUR");
        //    Currency targetCurrency = new Currency("GBP");

        //    var result = converter.Convert(amountInEUR, sourceCurrency, targetCurrency);

        //    Assert.Equal(0.8723m, exchangeRateEURtoGBP);
        //}

        //[Fact]
        //public void GetExchangeDKK_InvalidIsoCode_ShouldThrowException()
        //{
        //    var invalidIsoCode = "XYZ";

        //    var exception = Assert.Throws<ArgumentException>(() => Currency.GetCurrency(invalidIsoCode, _listCurreny));

        //    Assert.Equal("Currency with ISO code 'XYZ' not found.", exception.Message);
        //}

        //[Fact]
        //public void Convert_SameCurrency_ShouldReturnSameAmount()
        //{
        //    var currency = new Currency("EUR");

        //    var pair = new CurrencyPair(currency, currency, 1m, _listCurreny);

        //    var converter = new CurrencyConverter();
        //    var result = converter.Convert(100, pair);

        //    Assert.Equal(100m, result);
        //}

        //[Fact]
        //public void Convert_UnknownCurrency_ShouldThrowException()
        //{
        //    var invalidIsoCode = new Currency("XYZ");
        //    var validCurrency = new Currency("EUR");


        //    var pair = new CurrencyPair(validCurrency, invalidIsoCode, 1m, _listCurreny);

        //    var converter = new CurrencyConverter();

        //    var exception = Assert.Throws<ArgumentException>(() => converter.Convert(100, pair));
        //    Assert.Equal("Currency with ISO code 'XYZ' not found.", exception.Message);
        //}


        //[Fact]
        //public void Convert_EURtoUSD_ShouldReturnCorrectAmount()
        //{
        //    // Arrange
        //    var currencyEUR = new Currency("EUR");
        //    var currencyUSD = new Currency("USD");

        //    var rateEURtoDKK = ExchangeRateMock.GetExchangeDKK(currencyEUR);
        //    var rateUSDtoDKK = ExchangeRateMock.GetExchangeDKK(currencyUSD);

        //    var amount = 100m; // Przyk³adowa kwota w EUR

        //    // Act
        //    var result = Math.Round((amount * rateEURtoDKK) / rateUSDtoDKK, 4);

        //    // Assert
        //    Assert.Equal(1.1218m, result); // Oczekiwany kurs EUR ? USD przy obecnych danych
        //}

    }
}