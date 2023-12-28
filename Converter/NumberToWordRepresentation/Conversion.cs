using System.Globalization;

namespace NumberToWordRepresentation
{
    public static class Conversion
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

        public static string ToWordsEnglish(this long number)
        {
            if (!IsValid(number))
            {
                throw new InvalidOperationException("Conversion Failed, Only long datatype numbers are allowed");
            }
            if (number == 0L)
                return "Zero";

            string words = "";
            var groups = CreateGroups(number);

            foreach (var group in groups.Where(x => x.Item1 > 0))
            {
                words += $"{ConvertThreeDigitGroup((int)group.Item1)} {group.Item2} ";
            }

            return words.Trim();
        }
        public static string ToWordsEnglish(this double number)
        {
            if (!IsValid(number))
            {
                throw new InvalidOperationException("Conversion Failed, Only double datatype are allowed");
            }
            if (number == 0L)
                return zero;

            string word = ((long)number).ToWordsEnglish();

            if (number % 1 != 0)
            {
                word += $" Point ";
                string fractionNumber = number.ToString().Split('.')[1];
                if (Convert.ToDecimal(fractionNumber) == 0L)
                    return zero;

                for (int i = 0; i < fractionNumber.Length; i++)
                {
                    int decimalDigit = CharUnicodeInfo.GetDecimalDigitValue(fractionNumber[i]);
                    string unit = UnitsArray[decimalDigit];
                    word += string.IsNullOrEmpty(unit) ? $"{zero} " : $"{unit} ";
                }
            }

            return word.Trim();
        }



        private static List<Tuple<long, string>> CreateGroups(long number)
        {
            return new List<Tuple<long, string>>
            {
                Tuple.Create(number / 1_000_000_000_000_000_000L, "Quintillion"),
                Tuple.Create((number / 1_000_000_000_000_000L) % 1_000, "Quadrillion"),
                Tuple.Create((number / 1_000_000_000_000L) % 1_000, "Trillion"),
                Tuple.Create((number / 1_000_000_000L) % 1_000, "Billion"),
                Tuple.Create((number / 1_000_000L) % 1_000, "Million"),
                Tuple.Create((number / 1_000L) % 1_000, "Thousand"),
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
