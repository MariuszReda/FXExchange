using FXExchange.Application.Interfaces;
using FXExchange.Application.Serivces;
using FXExchange.Config;
using FXExchange.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


string firstArg = string.Empty;
string secendArg = string.Empty;
decimal amountArg = 0;


ValidateArguments(args, out firstArg, out secendArg, out amountArg);

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
    .Services
    .AddSingleton<IExchangeService, ExchangeService>();

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

var exchangeService = serviceProvider.GetRequiredService<IExchangeService>();
var result = exchangeService.PerformExchange(firstArg, secendArg, amountArg);

Console.WriteLine(result);


#region Methods

static void ValidateArguments(string[] args, out string firstArg, out string secendArg, out decimal amountArg)
{
    if (args.Length != 2)
    {
        Console.WriteLine("Usage: Exchange <currency pair> <amount to exchange>");
        Environment.Exit(1); 
    }

    if (!args[0].Contains('/'))
    {
        Console.WriteLine("Invalid currency pair format. Use format: <source>/<target> (e.g., EUR/DKK).");
        Environment.Exit(1); 
    }

    var pair = args[0].Split('/');
    if (pair.Length != 2)
    {
        Console.WriteLine("Invalid currency pair format. Use format: <source>/<target> (e.g., EUR/DKK).");
        Environment.Exit(1);
    }

    firstArg = pair[1].ToUpper();
    secendArg = pair[0].ToUpper();

    if (!decimal.TryParse(args[1], out amountArg))
    {
        Console.WriteLine("Invalid amount. Please provide a numeric value.");
        Environment.Exit(1);
    }
}

#endregion