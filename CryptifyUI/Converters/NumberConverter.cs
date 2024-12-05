using System;
using System.Globalization;
using System.Windows.Data;

namespace Cryptify.Converters
{
	public class NumberConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null || !IsNumericType(value))
				return value; 

			double number = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);

			if (Math.Abs(number) >= 1_000_000_000_000)
				return $"${number / 1_000_000_000_000:0.#}T";
			if (Math.Abs(number) >= 1_000_000_000) 
				return $"${number / 1_000_000_000:0.#}B";
			if (Math.Abs(number) >= 1_000_000)
				return $"${number / 1_000_000:0.#}M";
			if (Math.Abs(number) >= 1_000) 
				return $"${number / 1_000:0.#}K";
			if (Math.Abs(number) <= 1)
				return $"${number / 1:0.###}";

			return $"${number:0.#}";
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
				return null;

			string stringValue = value.ToString()!;

			if (stringValue.EndsWith("T", StringComparison.InvariantCulture) && 
			    stringValue.StartsWith("$", StringComparison.InvariantCulture))
			{
				string numberPart = stringValue[1..^1]; 

				if (double.TryParse(numberPart, NumberStyles.Float, CultureInfo.InvariantCulture, out double number))
				{
					return number; 
				}
			}

			return value;
		}

		private static bool IsNumericType(object value)
		{
			return value is byte or sbyte
				or short or ushort
				or int or uint
				or long or ulong
				or float or double
				or decimal;
		}
	}
}