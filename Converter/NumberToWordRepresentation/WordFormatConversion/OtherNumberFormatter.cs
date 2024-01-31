using System.Globalization;
using System.Text;

namespace NumberToWordRepresentation.FormatConversion
{
    public static class OtherNumberFormatter
    {
        private static readonly string zero = "Zero";
        private static readonly string[] UnitsArray = { "",
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine" };

        private static readonly string[] TeensArray = { "",
            "Eleven",
            "Twelve",
            "Thirteen",
            "Fourteen",
            "Fifteen",
            "Sixteen",
            "Seventeen",
            "Eighteen",
            "Nineteen" };
        private static readonly string[] TensArray = { "",
            "Ten",
            "Twenty",
            "Thirty",
            "Forty",
            "Fifty",
            "Sixty",
            "Seventy",
            "Eighty",
            "Ninety" };

        public static string ToOtherFormat(this long number)
        {
            if (!IsValid(number))
            {
                throw new InvalidOperationException("Conversion Failed, Number too long or not valid");
            }
            if (number == 0L)
                return "Zero";

            StringBuilder words = new StringBuilder();
            var groups = CreateGroups(number);

            foreach (var group in groups.Where(x => x.Item1 > 0))
            {
                words.Append($"{ConvertThreeDigitGroup((int)group.Item1)} {group.Item2} ");
            }

            return words.ToString().Trim();
        }
        public static string ToOtherFormat(this double number)
        {
            if (!IsValid(number))
            {
                throw new InvalidOperationException("Conversion Failed, Number too long or not valid");
            }
            if (number == 0L)
                return zero;
            StringBuilder word = new StringBuilder();
            word.Append(((long)number).ToOtherFormat());

            word.Append(number.ConvertEngFraction());
            return word.ToString().Trim();
        }

        public static string ConvertEngFraction(this double number, bool currencyConversion = false)
        {
            if (number % 1 == 0)
            {
                return string.Empty;
            }

            var word = new StringBuilder();

            if (!currencyConversion)
            {
                word.Append(" Point ");
            }

            string fractionPart = ExtractFractionPart(number);

            if (decimal.Parse(fractionPart) == 0)
            {
                return zero;
            }

            ConvertFractionDigitsToWords(word, fractionPart);

            return word.ToString();
        }

        private static string ExtractFractionPart(double number)
        {
            string[] parts = number.ToString().Split('.');
            return parts.Length > 1 ? parts[1] : string.Empty;
        }

        private static void ConvertFractionDigitsToWords(StringBuilder word, string fractionPart)
        {
            foreach (char digit in fractionPart)
            {
                int decimalDigit = CharUnicodeInfo.GetDecimalDigitValue(digit);
                string unit = UnitsArray[decimalDigit];
                word.Append(string.IsNullOrEmpty(unit) ? $"{zero} " : $"{unit} ");
            }
        }


        private static List<Tuple<long, string>> CreateGroups(long number)
        {
            return new List<Tuple<long, string>>
            {
                Tuple.Create(number / 1_000_000_000_000_000_000L % 1_000, "Quintillion"),
                Tuple.Create(number / 1_000_000_000_000_000L % 1_000, "Quadrillion"),
                Tuple.Create(number / 1_000_000_000_000L % 1_000, "Trillion"),
                Tuple.Create(number / 1_000_000_000L % 1_000, "Billion"),
                Tuple.Create(number / 1_000_000L % 1_000, "Million"),
                Tuple.Create(number / 1_000L % 1_000, "Thousand"),
                Tuple.Create(number % 1_000, "")
            };
        }

        private static bool IsValid(long number)
        {
            return long.TryParse(number.ToString(), out _);
        }

        private static bool IsValid(double number)
        {
            return double.TryParse(number.ToString(), out _);
        }

        private static string ConvertThreeDigitGroup(int number)
        {
            string words = "";
            int hundreds = number / 100;
            int tens = number % 100;
            int units = number % 10;

            words += AppendHundreds(hundreds);
            words += AppendTensAndUnits(tens, units);

            return words.Trim();
        }

        private static string AppendHundreds(int hundreds)
        {
            string hundredUnit = "Hundred";
            return !string.IsNullOrEmpty(UnitsArray[hundreds]) ? $"{UnitsArray[hundreds]} {hundredUnit} " : "";
        }

        private static string AppendTensAndUnits(int tens, int units)
        {
            string words = "";

            words += tens >= 11 && tens <= 19 ? TeensArray[tens - 10] : $"{TensArray[tens / 10]} {UnitsArray[units]}";

            return words;
        }
    }
}
