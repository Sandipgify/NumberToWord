using NumberToWordRepresentation.FormatConversion;
using System.Text;

namespace NumberToWordRepresentation.CurrencyConversion
{
    public static class NepaliCurrencyFormatter
    {
        public static string NepaliCurrency(this long amount)
        {
            return BuildNepaliCurrencyString(amount, null);
        }

        public static string NepaliCurrency( this double amount)
        {
            var fraction = amount.ConvertNepFraction(true);
            return BuildNepaliCurrencyString(amount, fraction);
        }

        private static string BuildNepaliCurrencyString(double amount, string fraction)
        {
            var nepCurrency = new StringBuilder();

            nepCurrency.Append(((long)amount).NepaliNumberFormat());
            nepCurrency.Append(" Rupees");

            if (!string.IsNullOrEmpty(fraction))
            {
                nepCurrency.Append(" ").Append(fraction).Append("Paisa");
            }

            return nepCurrency.ToString();
        }
    }
}
