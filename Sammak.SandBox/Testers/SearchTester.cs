using Sammak.SandBox.Helpers;
using Sammak.SandBox.OptionalArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.Testers
{
    public class SearchTester
    {
        public static void Run()
        {
            new SearchTester().SearchWithOptionalArgsTest();
        }

        private void SearchWithOptionalArgsTest()
        {
            ISearch search = new Search();
            //var result = search.SearchWithOptionalArgs();
            //var result = search.SearchWithOptionalArgs(name: "myName");
            //var result = search.SearchWithOptionalArgs(inActive: true);
            //var result = search.SearchWithOptionalArgs(name: "myName", inActive: true);
            //var result = search.SearchWithOptionalArgs("myName");
            var result = search.SearchWithOptionalArgs(inActive: false);
            ConsoleDisplay.ShowObject(result, nameof(SearchWithOptionalArgsTest));
        }
    }
}
