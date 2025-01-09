using FXExchange.Application.Interfaces;
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

        public override bool Equals(object obj) => obj is Currency other && IsoCode == other.IsoCode;
        public override int GetHashCode() => IsoCode.GetHashCode();
    }

}
