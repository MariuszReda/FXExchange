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
