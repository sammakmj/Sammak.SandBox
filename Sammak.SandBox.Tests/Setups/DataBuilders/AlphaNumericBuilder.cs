using System;

namespace Sammak.SandBox.Tests.Setups.DataBuilders
{
    public static class AlphaNumericBuilder
    {
        #region Pupblic methods

        public static string BuildAlphaNumeric(Random random)
        {
            return AlphaNumeric.Build(random, 2);
        }

        public static string BuildNumeric(Random random)
        {
            return Numeric.Build(random, 2);
        }
        #endregion

    }
}
