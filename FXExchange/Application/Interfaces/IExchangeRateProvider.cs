using FXExchange.Domain;

namespace FXExchange.Application.Interfaces
{
    public interface IExchangeRateProvider
    {
        Task InitializeAsync();
        bool IsValidIsoCode(string isoCode);
        decimal GetExchangeRate(Currency source, Currency target);
    }
}
