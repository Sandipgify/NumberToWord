using NumberToWordRepresentation.FormatConversion;
using System.Text;

namespace NumberToWordRepresentation.CurrencyConversion
{
    public static class OtherCurrencyFormatter
    {
        public static string OtherCurrency(this long amount)
        {
            return BuildOtherCurrencyString(amount, null);
        }

        public static string OtherCurrency(this double amount)
        {
            var fraction = amount.ConvertEngFraction(true);
            return BuildOtherCurrencyString(amount, fraction);
        }

        private static string BuildOtherCurrencyString(double amount, string fraction)
        {
            var otherCurrency = new StringBuilder();
            otherCurrency.Append(((long)amount).ToOtherFormat());
            otherCurrency.Append(" Dollor");

            if (!string.IsNullOrEmpty(fraction))
            {
                otherCurrency.Append(" ").Append(fraction).Append("Cent");
            }

            return otherCurrency.ToString();
        }
    }
}
