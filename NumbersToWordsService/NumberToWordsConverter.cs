using System;
using System.Collections.Generic;
using System.Text;

namespace NumbersToWordsService
{
    public class NumberToWordsConverter
    {
        private Dictionary<int, string> _digits = new Dictionary<int, string>
        {
            { 1, "one" },
            { 2, "two" },
            { 3, "three" },
            { 4, "four" },
            { 5, "five" },
            { 6, "six" },
            { 7, "seven" },
            { 8, "eight" },
            { 9, "nine" },
            { 10, "ten" },
            { 11, "eleven" },
            { 12, "twelve" },
            { 13, "thirteen" },
            { 14, "fourteen" },
            { 15, "fifteen" },
            { 16, "sixteen" },
            { 17, "seventeen" },
            { 18, "eighteen" },
            { 19, "nineteen" },
        };

        private Dictionary<int, string> _tenths = new Dictionary<int, string>
        {
            { 2, "twenty" },
            { 3, "thirty" },
            { 4, "forty" },
            { 5, "fifty" },
            { 6, "sixty" },
            { 7, "seventy" },
            { 8, "eighty" },
            { 9, "ninety" },
        };

        public string Convert(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            int separatorIndex = input.IndexOf(',');
            int fractionValue = 0;
            int parsedNumberWithoutFraction = 0;
            string fractionStr = string.Empty;

            input = input.Trim().Replace(" ", string.Empty);

            if (separatorIndex > 0)
            {
                var splittedNumber = input.Split(',');
                fractionStr = splittedNumber[1];
                if (fractionStr.Length == 1)
                    fractionStr += "0";

                if (!int.TryParse(splittedNumber[1], out fractionValue)) return string.Empty;
                if (!int.TryParse(splittedNumber[0], out parsedNumberWithoutFraction)) return string.Empty;
            }
            else if (!int.TryParse(input, out parsedNumberWithoutFraction)) return string.Empty;

            StringBuilder _sb = new StringBuilder();

            if (parsedNumberWithoutFraction == 0)
            {
                _sb.Append("zero");
            }
            else
            {
                input = $"{parsedNumberWithoutFraction:000000000}";
                var numberLength = 0;

                if (numberLength > 0)
                    input = input.Substring(0, numberLength);

                if (input[0] != '0' || input[1] != '0' || input[2] != '0')
                    _sb.Append(ConvertMilions(input.Substring(0, 3)));
                if (input[3] != '0' || input[4] != '0' || input[5] != '0')
                    _sb.Append(ConvertThousands(input.Substring(3, 3)));
                _sb.Append(ConvertTenths(input.Substring(6, 3)));
            }

            if (parsedNumberWithoutFraction == 1)
                _sb.Append(" dollar");
            else
                _sb.Append(" dollars");

            if (fractionValue > 0)
            {
                _sb.Append(" and ");
                _sb.Append(ConvertFractions(fractionStr));
            }

            return _sb.ToString().Trim().Replace("  ", " ");
        }

        private string ConvertFractions(string input)
        {
            string result = string.Empty;
            if (!int.TryParse(input, out int fraction)) return string.Empty;

            if (fraction > 20 && int.TryParse(input[0].ToString(), out int tenths))
            {
                if (!_tenths.TryGetValue(tenths, out string tenthsOutput)) return string.Empty;

                result += $"{tenthsOutput}";
                if (fraction % 10 != 0)
                    result += "-";
            }

            var valueForParse = fraction > 10 && fraction < 20 ? input.Substring(0, 2) : input.Substring(1, 1);
            if (!int.TryParse(valueForParse, out int fraction2)) return string.Empty;

            if (!_digits.TryGetValue(fraction2, out string output))
            {
                if (fraction == 0 || !_digits.TryGetValue(fraction, out output))
                    return string.Empty;
            }

            if (fraction == 1)
                result += $"{output} cent";
            else if (fraction == 0)
                result += $" cents";
            else
                result += $"{output} cents";

            return result;
        }


        private string ConvertTenths(string input)
        {
            string result = string.Empty;
            if (!int.TryParse(input, out int value)) return string.Empty;

            if (value >= 100 && int.TryParse(input[0].ToString(), out int hundreds))
            {
                if (!_digits.TryGetValue(hundreds, out string hundredsOutput)) return string.Empty;

                result += $" {hundredsOutput} hundred";
            }

            if (!int.TryParse(input.Substring(1, 2), out value)) return string.Empty;
            
            if (value > 20 && int.TryParse(input[1].ToString(), out int tenths))
            {
                if (!_tenths.TryGetValue(tenths, out string tenthsOutput)) return string.Empty;

                result += $" {tenthsOutput}";

                if (value % 10 != 0)
                    result += "-";
            }

            var valueForParse = value > 10 && value < 20 ? input.Substring(1, 2) : input.Substring(2, 1);
            if (!int.TryParse(valueForParse, out value)) return string.Empty;

            if (value == 0)
            {
                return $"{result}";
            }

            if (!_digits.TryGetValue(value, out string output)) return string.Empty;

            result += $"{output}";

            return result;
        }
        private string ConvertThousands(string input)
        {
            string result = string.Empty;
            if (!int.TryParse(input, out int value)) return string.Empty;

            if (value > 100 && int.TryParse(input[0].ToString(), out int hundreds))
            {
                if (!_digits.TryGetValue(hundreds, out string hundredsOutput)) return string.Empty;

                result += $" {hundredsOutput} hundred ";
            }

            if (!int.TryParse(input.Substring(1, 2), out value)) return string.Empty;

            var valueForParse = value > 10 && value < 20 ? input.Substring(1, 2) : input.Substring(1, 1);
            if (value > 20 && int.TryParse(input[1].ToString(), out int tenths))
            {
                if (!_tenths.TryGetValue(tenths, out string tenthsOutput)) return string.Empty;

                result += $" {tenthsOutput}";

                if (value % 10 != 0)
                    result += "-";
            }

            valueForParse = value > 10 && value < 20
                ? input.Substring(1, 2) 
                : result.Contains("-") ? input.Substring(2, 1) : input.Substring(1, 1);

            if (!int.TryParse(valueForParse, out value)) return string.Empty;

            if (!_digits.TryGetValue(value, out string output)) return string.Empty;

            result += $"{output} thousand";

            return result;
        }

        private string ConvertMilions(string input)
        {
            string result = string.Empty;
            if (!int.TryParse(input, out int value)) return string.Empty;

            if (value > 100 && int.TryParse(input[0].ToString(), out int hundreds))
            {
                if (!_digits.TryGetValue(hundreds, out string hundredsOutput)) return string.Empty;

                result += $" {hundredsOutput} hundred";
            }

            if (!int.TryParse(input.Substring(1, 2), out value)) return string.Empty;

            var valueForParse = value > 10 && value < 20 ? input.Substring(1, 2) : input.Substring(1, 1);
            if (value > 20 && int.TryParse(valueForParse, out int tenths))
            {
                if (!_tenths.TryGetValue(tenths, out string tenthsOutput)) return string.Empty;

                result += $" {tenthsOutput}";

                if (value % 10 != 0)
                    result += "-";
            }

            valueForParse = value > 10 && value < 20
                ? input.Substring(1, 2)
                : result.Contains("-") ? input.Substring(2, 1) : input.Substring(1, 1);
            
            if (!int.TryParse(valueForParse, out value)) return string.Empty;

            if (!_digits.TryGetValue(value, out string output)) return string.Empty;

            result += $" {output} million";

            return result;
        }
    }
}