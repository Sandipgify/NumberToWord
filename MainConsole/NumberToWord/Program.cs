using NumberToWordRepresentation.CurrencyConversion;
using NumberToWordRepresentation.FormatConversion;

double intdata = 123456;
double decimaldata = 123456.0989;

Console.WriteLine(intdata.ToOtherFormat());
Console.WriteLine(decimaldata.ToOtherFormat());
Console.WriteLine(intdata.NepaliNumberFormat());
Console.WriteLine(decimaldata.NepaliNumberFormat());
Console.WriteLine(intdata.NepaliCurrency());
Console.WriteLine(decimaldata.NepaliCurrency());
Console.WriteLine(intdata.OtherCurrency());
Console.WriteLine(decimaldata.OtherCurrency());

