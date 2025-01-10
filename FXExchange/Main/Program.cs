using FXExchange.Application.Interfaces;
using FXExchange.Config;
using FXExchange.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Config\\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceProvider = new ServiceCollection()
    .Configure<CurrencyApiConfig>((configuration.GetSection("CurrencyApi")));

serviceProvider.AddSingleton<IExchangeRateProvider, MockExchangeRateProvider>();
//serviceProvider.AddSingleton<IExchangeRateProvider, ExchangeRateProvider>();

serviceProvider.AddHttpClient<ExchangeRateProvider>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.exchangerate-api.com/v4/latest/");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
});

var service = serviceProvider.BuildServiceProvider();

//var test = service.GetRequiredService<ExchangeRateProvider>();
//test.FetchRatesFromApi();


////Console.Write("Enter the first currency code (e.g., EUR): ");
////string firstCurrencyCode = Console.ReadLine()?.ToUpper();

////// Przyjmowanie drugiego kodu waluty
////Console.Write("Enter the second currency code (e.g., USD): ");
////string secondCurrencyCode = Console.ReadLine()?.ToUpper();

////// Przyjmowanie liczby
////Console.Write("Enter the amount to exchange: ");
////if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
////{
////    Console.WriteLine("Invalid amount entered. Please provide a valid number.");
////    return;
////}

////// Wyświetlenie wartości wejściowych
////Console.WriteLine("\nInput Summary:");
////Console.WriteLine($"First Currency Code: {firstCurrencyCode}");
////Console.WriteLine($"Second Currency Code: {secondCurrencyCode}");
////Console.WriteLine($"Amount: {amount}");