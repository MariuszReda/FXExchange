using FXExchange.Application.Interfaces;
using FXExchange.Config;
using FXExchange.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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

serviceProvider.BuildServiceProvider();