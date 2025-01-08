using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXExchange.Domain
{
    public class Currency
    {
        public string IsoCode { get; }
        public Currency(string isoCode)
        {
            if (string.IsNullOrWhiteSpace(isoCode))
                throw new ArgumentException("ISO code cannot be null or empty.");
            IsoCode = isoCode.ToUpper();
        }

        public static Currency GetCurrency(string isoCode, List<Currency> currencies)
        {
            if (currencies == null || currencies.Count == 0)
                throw new ArgumentException("The list of currencies cannot be null or empty.");

            var currency = currencies.FirstOrDefault(c => c.IsoCode == isoCode.ToUpper());
            if (currency == null)
                throw new ArgumentException($"Currency with ISO code '{isoCode}' not found.");

            return currency;
        }

        public override bool Equals(object obj) => obj is Currency other && IsoCode == other.IsoCode;
        public override int GetHashCode() => IsoCode.GetHashCode();
    }

}
