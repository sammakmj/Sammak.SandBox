using System;
using Sammak.SandBox.Helpers;

namespace Sammak.SandBox.Testers
{
    public class CheckedUncheckedTester
    {
        public static void Run()
        {
            new CheckedUncheckedTester().StringConcatTest();
        }

        private void StringConcatTest()
        {
            var FulfilledDate = DateTime.Now;
            var FulfilledBy = "MJS";
            var Number = 5;
            var Id = 55;
            var result = "Order ";
            result += $"Id: {Id}";
            result += $", Number: {Number}";
            result += $", FulfilledDate: {FulfilledDate}";
            result += $", FulfilledBy: {FulfilledBy}";
            ConsoleDisplay.ShowObject(result, nameof(result));
        }

        private void ConstArithmatickTest()
        {
            unchecked
            {
                const int E_FAIL = (int)0x80004005;
                ConsoleDisplay.ShowObject(E_FAIL, nameof(E_FAIL));
            }
        }
    }
}
