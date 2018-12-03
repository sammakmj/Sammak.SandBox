using NetTools;
using Newtonsoft.Json;
using Sammak.SandBox.Helpers;
using Sammak.SandBox.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sammak.SandBox.Testers
{
    public class TestType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public decimal Dec { get; set; }
    }

    public class ServiceTester
    {
        public static void Run()
        {
            new ServiceTester().NullGuidTest();
        }

        private void NullGuidTest()
        {
            Guid? testGuid = null;
            //var result = testGuid.IsNullOrEmpty();
            Guid guid2 = Guid.NewGuid();
            var result = guid2.IsNullOrEmpty();
            ConsoleDisplay.ShowObject(result, nameof(result));
        }

        private void EmptyQueryStringTest()
        {
            var parameters = new Dictionary<string, string>
            {
            };
            var path = "emptyParamTest";
            var pathAndQuery = path.AddQueryString(parameters);
            ConsoleDisplay.ShowObject(pathAndQuery, nameof(pathAndQuery));
        }

        private void DictionaryToObjectTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "id", "54"},
                { "ham", "Glaced?"},
                { "x-men", "Wolverine + Logan"},
                { "Dob", "2/13/2018" },
                { "Dec", "123" },
            };
            parameters["name"] = 43.ToString();
            var testType = parameters.DictionaryToObject<TestType>();
            ConsoleDisplay.ShowObject(testType, nameof(testType));
        }

        private void ConvertPropertiesTest()
        {
            var someObject = new 
            {
                Id = 5,
                Name = "Some Object",
                Dob = DateTime.Now,
                Dec = 123
            };

            var some = someObject.PropertiesToDictionary();
            ConsoleDisplay.ShowObject(some, nameof(some));
        }

        private void QueryStringTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "id", "54"},
                //{ "ham", "Glaced?"},
                //{ "x-men", "Wolverine + Logan"},
                //{ "Time", DateTime.UtcNow.ToString() },
            };
            parameters["name"] = 43.ToString();
            var path = "validatetoken";
            //var pathWithParameters = path.AddQueryString(parameters);
            //ConsoleDisplay.ShowObject(pathWithParameters, nameof(pathWithParameters));

            var rootUri = RootUrl();
            using (var client = GetHttpClient(rootUri))
            {
                var rootUrl = $"{rootUri}{path}";
                var queryString = parameters.ToQueryString();
                ConsoleDisplay.ShowObject(queryString, nameof(queryString));
                var pathAndQuery = path.AddQueryString(parameters);
                ConsoleDisplay.ShowObject(pathAndQuery, nameof(pathAndQuery));

                ConsoleDisplay.ShowObject(client.BaseAddress, nameof(client.BaseAddress));
                //string url = BuildUrlWithQueryString(rootUrl, pathWithParameters, port);
                string url = rootUrl.ToUrlWithQueryString(queryString);
                ConsoleDisplay.ShowObject(url, nameof(url));
                var myUri = new Uri(url);
                //ConsoleDisplay.ShowObject(myUri, nameof(myUri));
                ConsoleDisplay.ShowObject(myUri.PathAndQuery, nameof(myUri.PathAndQuery));
                ConsoleDisplay.ShowObject(myUri.Port, nameof(myUri.Port));
                //ConsoleDisplay.ShowObject(myUri.Query, nameof(myUri.Query));
                //ConsoleDisplay.ShowObject(myUri.AbsolutePath, nameof(myUri.AbsolutePath));
                //ConsoleDisplay.ShowObject(myUri.AbsoluteUri, nameof(myUri.AbsoluteUri));
                //ConsoleDisplay.ShowObject(myUri.Authority, nameof(myUri.Authority));
                //ConsoleDisplay.ShowObject(myUri.Host, nameof(myUri.Host));
                //ConsoleDisplay.ShowObject(myUri.LocalPath, nameof(myUri.LocalPath));
                //ConsoleDisplay.ShowObject(myUri.OriginalString, nameof(myUri.OriginalString));
            }
        }

        private HttpClient GetHttpClient(string rootUri)
        {
            HttpClient Client;

            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };
            Client = new HttpClient(handler)
            {
                BaseAddress = new Uri(rootUri)
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return Client;
        }

        private static string RootUrl()
        {
            var authHost = ConfigurationManager.AppSettings["Auth.Host"];
            // NOTE: the root url should end with a "/" so that the path would be properlty appended by the HttpClient class to form a full url.
            // if the ending slash is missing, the last word after the last existing slash would be dropped, then the path gets appended rendering a wrong url.
            var authApiPrefix = "api/auth/";

            if (string.IsNullOrWhiteSpace(authHost) || authHost.Length < 1)
            {
                throw new Exception("The 'Auth.Host' URL define is missing from the config file!");
            }

            // NOTE: the 'Auth.Port' and/or 'Auth.ApiPrefix' defines optiaonly could be missing, 
            // in  which case the 'Auth.Host' should hold the full path of the host url
            return $"{authHost}/{authApiPrefix}";
        }

        private void IPAddressConverterTest()
        {
            var endpoints = new IPEndPoint[]
            {
                new IPEndPoint(IPAddress.Parse("8.8.4.4"), 53),
                new IPEndPoint(IPAddress.Parse("2001:db8::ff00:42:8329"), 81)
            };

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPAddressConverter());
            settings.Converters.Add(new IPEndPointConverter());
            settings.Formatting = Formatting.Indented;

            string json = JsonConvert.SerializeObject(endpoints, settings);
            Console.WriteLine(json);

            var endpoints2 = JsonConvert.DeserializeObject<IPEndPoint[]>(json, settings);

            foreach (IPEndPoint ep in endpoints2)
            {
                Console.WriteLine();
                Console.WriteLine($"AddressFamily: {ep.AddressFamily}");
                Console.WriteLine($"Address: {ep.Address}");
                Console.WriteLine($"Port: {ep.Port}");
            }
        }

        private void IPAddressTest()
        {
            var ip = "13.68.136.211/8";
            var address = IPAddressRange.Parse(ip);
            ConsoleDisplay.ShowObject(address, nameof(address));
            var shouldPerformSSO = ShouldPerformSSO("::1");
            ConsoleDisplay.ShowObject(shouldPerformSSO, nameof(shouldPerformSSO));
        }

        private bool ShouldPerformSSO(string requestUserHostAddress)
        {
            //try
            //{
                var ips = ApplicationHelper.GetAppSettingValue("SingleSignOnIPs")?.Split(';');

                if (ips != null)
                {
                    List<IPAddressRange> ranges = new List<IPAddressRange>();

                    foreach (var ipRange in ips)
                    {
                        var address = IPAddressRange.Parse(ipRange);
                        ConsoleDisplay.ShowIPAddress(address, nameof(address));
                        ranges.Add(IPAddressRange.Parse(ipRange));
                    }

                    IPAddress clientIpAddress = null;

                    IPAddress.TryParse(requestUserHostAddress, out clientIpAddress);
                    ConsoleDisplay.ShowIPAddress(clientIpAddress, nameof(clientIpAddress));
                    var ip = ranges[4];
                    var ipAddr = clientIpAddress;
                    var yes = ip.Contains(clientIpAddress);

                    if (ranges.Any(range => range.Contains(clientIpAddress)))
                    {
                        //LoginEvents webEvent = new LoginEvents("IP found in SSO Range ", null);
                        //webEvent.Raise();
                        return true;
                    }
                    else
                    {
                        //LoginEvents webEvent = new LoginEvents("IP not found in SSO Range " + Request.UserHostAddress, null);
                        //webEvent.Raise();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}
        }

        private void ServiceConstructorTest()
        {
            var service = new AuthHttpService();
            ConsoleDisplay.ShowObject(service, nameof(service));
        }
    }
}
