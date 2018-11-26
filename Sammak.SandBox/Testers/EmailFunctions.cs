using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sammak.SandBox.Testers
{
    internal class EmailFunctions
    {
        private static readonly Dictionary<string, bool> testEmails = new Dictionary<string, bool>
        {
            { @"NotAnEmail",                    false },
            { @"@NotAnEmail",                   false },
            { @"""test\\blah""@example.com",    true },
            { @"""test\blah""@example.com",     false },
            { "\"test\\\rblah\"@example.com",   true },
            { "\"test\rblah\"@example.com",     false },
            { @"""test\""blah""@example.com",   true },
            { @"""test""blah""@example.com",    false },
            { @"customer/department@example.com", true },
            { @"$A12345@example.com",           true },
            { @"!def!xyz%abc@example.com",      true },
            { @"_Yosemite.Sam@example.com",     true },
            { @"~@example.com",                 true },
            { @".wooly@example.com",            false },
            { @"wo..oly@example.com",           false },
            { @"pootietang.@example.com",       false },
            { @".@example.com",                 false },
            { @"""Austin@Powers""@example.com", true },
            { @"Ima.Fool@example.com",          true },
            { @"""Ima.Fool""@example.com",      true },
            { @"""Ima Fool""@example.com",      true },
            { @"Ima Fool@example.com",          false },
        };

        // Note: this regex validate email addresses that conform to the email format standard defined in the RFC 2822(https://tools.ietf.org/html/rfc2821)
        private static readonly string emailRegexPattern = 
            @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        const string _expression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";


        public static void Run()
        {
            new EmailFunctions().EmailCheck();
        }

        private void EmailCheck()
        {

            Regex regex = new Regex(emailRegexPattern, RegexOptions.IgnoreCase);

            foreach(var email in testEmails)
            {
                //var match = ValidateEmail(email.Key);
                var match = ValidateStringAgainstRegex(email.Key, _expression);
                //var match = regex.IsMatch(email.Key);
                Console.WriteLine($"{email.Key}  - Expected: {email.Value} - Result: {match}");
            }
        }

        private void ValidateAnEmail()
        {
            string emailAddress = @"Last, First test@test.com";
            var match = ValidateEmail(emailAddress);
            Console.WriteLine($"{emailAddress}  - Validation is: {match}");

            //var pattern = @"^(?<name1>[a-zA-Z0-9]+?),? (?<name2>[a-zA-Z0-9]+?),? (?<address1>[a-zA-Z0-9.-_<>]+?)$";
            match = ValidateStringAgainstRegex(emailAddress, _expression);
            Console.WriteLine($"{emailAddress} - Validation against pattern: {match}");

        }

        public bool ValidateEmail(string emailAddress)
        {
            Regex regex = new Regex(emailRegexPattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(emailAddress);
        }

        public bool ValidateStringAgainstRegex(string stringToValidate, string regExPattern)
        {
            Regex regex = new Regex(regExPattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(stringToValidate);
        }
    }
}
