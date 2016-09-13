using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseM
{
    public static class Translator
    {
        public const string ZERO = "0";
        public const string UNKNOWN = "Unknown";
        public static string Translate(string number, NumericSystem fromSystem, NumericSystem toSystem)
        {
            if (number == null)
                throw new ArgumentNullException("number");
            if (number == string.Empty)
                return "0";
            if (!IsGoodNumber(number, fromSystem))
                return UNKNOWN;

            string result = ChangeToDecimal(number, fromSystem);
            long dec;

            if (result == UNKNOWN)
                return result;
            else
                dec = Convert.ToInt64(result);

            StringBuilder builder = new StringBuilder();
            int digits = (int)toSystem;

            do
            {
                long remainder = dec % digits;
                builder.Insert(0, ConvertToOneDigit(remainder));
                dec /= digits;
            } while (dec > 0);

            return builder.ToString();
        }

        private static string ChangeToDecimal(string number, NumericSystem originalSystem)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException("number", "'number' can't be null or empty.");

            double systemBase = Convert.ToDouble(originalSystem);

            if (originalSystem == NumericSystem.Decimal)
            {
                long result;
                bool succeed = long.TryParse(number, out result);
                if (succeed)
                    return result.ToString();
                else
                    return UNKNOWN;
            }
            else
            {
                // make sure it's done from Least Important to Most
                number = number.Reverse();
                long totalNumber = 0;

                for (int i = 0; i < number.Length; i++)
                {
                    long weight = Convert.ToInt32(Math.Pow(systemBase, i));
                    totalNumber += CharToInt(number[i]) * weight;
                }

                return totalNumber.ToString();
            }

        }
        private static int CharToInt(char number)
        {
            number = number.ToString().ToUpper()[0];
            char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F' };
            if (!letters.Contains(number))
                return int.Parse(number.ToString());
            else
            {
                for (int i = 0; i < letters.Count(); i++)
                {
                    if (letters[i] == number)
                        return i + 10; // because A is the tenth number
                }
            }

            throw new ArgumentException("'number' is not a valid digit (0-F)");
        }
        private static char ConvertToOneDigit(long number)
        {
            char[] possibleDigits = { '0', '1', '2', '3', '4', '5',
                '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

            if (number > possibleDigits.Length)
                throw new ArgumentException("The number should not be greater than 15");

            return (possibleDigits[number]);
        }
        private static bool IsGoodNumber(string number, NumericSystem system)
        {
            char[] possibleDigits = { '0', '1', '2', '3', '4', '5',
                '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int max = (int)system;
             
            for (int i = 0; i < number.Length; i++)
            {
                int index = 0;

                for (int j = 0; j < possibleDigits.Length; j++)
                {
                    if (possibleDigits[j] == number[i])
                        index = j;
                }

                if (index > max)
                    return false;
            }

            return true;
        }
        private static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
