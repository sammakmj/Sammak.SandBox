using System;

namespace Sammak.SandBox.Tests.Setups.DataBuilders
{
    public class AlphaNumeric
    {
        public static string Build(Random random, int length)
        {
            var stringChars = new char[length];

            //pick up an uppercase char for the first charcter in the result string
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var maxLen = chars.Length;
            stringChars[0] = chars[random.Next(maxLen)];

            //the next characters in the result could be upper, lower or number
            chars += "0123456789abcdefghijklmnopqrstuvwxyz";
            maxLen = chars.Length;

            for (int i = 1; i < length; i++)
            {
                stringChars[i] = chars[random.Next(maxLen)];
            }

            return new string(stringChars);
        }
    }
}
