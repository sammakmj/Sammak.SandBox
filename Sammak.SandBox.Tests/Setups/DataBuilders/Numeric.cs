using System;

namespace Sammak.SandBox.Tests.Setups.DataBuilders
{
    public class Numeric
    {
        public static string Build(Random random, int length)
        {
            var stringChars = new char[length];

            //pick up non-zero char for the first charcter in the result string
            var chars = "123456789";
            var maxLen = chars.Length;
            stringChars[0] = chars[random.Next(maxLen)];

            //the next characters in the result could be any number
            chars = "0123456789";
            maxLen = chars.Length;

            for (int i = 1; i < length; i++)
            {
                stringChars[i] = chars[random.Next(maxLen)];
            }

            return new String(stringChars);
        }
    }
}
