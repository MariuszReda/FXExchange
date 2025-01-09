using FXExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Application.Interfaces
{
    public interface IExchangeRateProvider
    {
        Dictionary<string, decimal> GetRates();
        bool IsValidIsoCode(string isoCode);
        decimal GetExchangeRate(Currency currency);
    }
}
