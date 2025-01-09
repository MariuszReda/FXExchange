using FXExchange.Application.Interfaces;
using FXExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Infrastructure
{
    public class CurrencyFactory
    {
        private readonly IExchangeRateProvider _provider;

        public CurrencyFactory(IExchangeRateProvider provider)
        {
            _provider = provider;
        }

        public Currency Create(string isoCode)
        {
            if (!_provider.IsValidIsoCode(isoCode))
                throw new ArgumentException($"ISO code '{isoCode}' is not supported.");

            return new Currency(isoCode);
        }
    }
}
