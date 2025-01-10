using FXExchange.Application.Interfaces;
using FXExchange.Config;
using FXExchange.Domain;
using FXExchange.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


if (args.Length != 2)
{
    Console.WriteLine("Usage: Exchange <currency pair> <amount to exchange>");
    return;
}

string firstArg="";
string secendArg="";
decimal amountArg = 0;

foreach (var arg in args)
{
    var pair = args[0].Split('/');
    firstArg = pair[1];
    secendArg = pair[0];

    if (!decimal.TryParse(args[1], out amountArg))
    {
        Console.WriteLine("Invalid amount. Please provide a numeric value.");
        return;
    }
}

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Config\\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection()
    .Configure<CurrencyApiConfig>(configuration.GetSection("CurrencyApi"))
    .AddHttpClient<ExchangeRateProvider>((provider, httpClient) =>
    {
        var apiConfig = provider.GetRequiredService<IOptions<CurrencyApiConfig>>().Value;
        httpClient.BaseAddress = new Uri($"https://v6.exchangerate-api.com/v6/{apiConfig.ApiKey}/latest/DKK");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    })
    .Services;

bool useMockData = configuration.GetValue<bool>("UseMockData");

    if (useMockData)
    {
        services.AddSingleton<IExchangeRateProvider, MockExchangeRateProvider>();
    }
    else
    {
        services.AddSingleton<IExchangeRateProvider>(provider =>
        {
            var exchangeRateProvider = provider.GetRequiredService<ExchangeRateProvider>();
            exchangeRateProvider.InitializeAsync().Wait(); 
            return exchangeRateProvider;
        });
    }
    

var serviceProvider = services.BuildServiceProvider();


var exchangeRateProvider = serviceProvider.GetRequiredService<IExchangeRateProvider>();
CurrencyFactory factory = new CurrencyFactory(exchangeRateProvider);
Currency sourceCurrency = factory.Create(firstArg);
Currency targetCurrency = factory.Create(secendArg);
decimal exchangeRate = exchangeRateProvider.GetExchangeRate(sourceCurrency, targetCurrency);
CurrencyPair currencyPair = new CurrencyPair(sourceCurrency, targetCurrency, exchangeRate);
var result = currencyPair.Convert(amountArg);
Console.WriteLine(result);
