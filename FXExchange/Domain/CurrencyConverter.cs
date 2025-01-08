using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Domain
{
    public class CurrencyConverter
    {
        public decimal Convert(decimal amount, CurrencyPair pair)
        {
            if (pair.BaseCurrency.Equals(pair.QuoteCurrency))
                return amount;

            return amount * pair.ExchangeRate;
        }
    }

}
