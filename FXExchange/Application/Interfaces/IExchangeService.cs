using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Application.Interfaces
{
    public interface IExchangeService
    {
        decimal PerformExchange(string sourceCurrencyCode, string targetCurrencyCode, decimal amount);
    }
}
