using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;

namespace Sammak.SandBox.Helpers
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            // we're only getting the first description we find
                            // others will be ignored
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            return description;
        }

        public static bool IsValidUrl(this string url)
        {
            var decodedUrl = HttpUtility.UrlDecode(url);
            return Uri.IsWellFormedUriString(decodedUrl, UriKind.Absolute); ;
        }

        //public static string BuildQueryString(this string initialQueryString, string name, string value)
        //{
        //    var sb = new StringBuilder();

        //    for (int i = 0; i < httpValueCollection.Count; i++)
        //    {
        //        string text = httpValueCollection.GetKey(i);
        //        {
        //            text = HttpUtility.UrlEncode(text);

        //            string val = (text != null) ? (text + "=") : string.Empty;
        //            string[] vals = httpValueCollection.GetValues(i);

        //            if (sb.Length > 0)
        //                sb.Append('&');

        //            if (vals == null || vals.Length == 0)
        //                sb.Append(val);
        //            else
        //            {
        //                if (vals.Length == 1)
        //                {
        //                    sb.Append(val);
        //                    sb.Append(HttpUtility.UrlEncode(vals[0]));
        //                }
        //                else
        //                {
        //                    for (int j = 0; j < vals.Length; j++)
        //                    {
        //                        if (j > 0)
        //                            sb.Append('&');

        //                        sb.Append(val);
        //                        sb.Append(HttpUtility.UrlEncode(vals[j]));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return sb.ToString();
        //}

        public static Uri AddQueryStringParam(this Uri uri, string name, string value)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);

            var ub = new UriBuilder(uri);

            // this code block is taken from httpValueCollection.ToString() method
            // and modified so it encodes strings with HttpUtility.UrlEncode
            if (httpValueCollection.Count == 0)
                ub.Query = String.Empty;
            else
            {
                var sb = new StringBuilder();

                for (int i = 0; i < httpValueCollection.Count; i++)
                {
                    string text = httpValueCollection.GetKey(i);
                    {
                        text = HttpUtility.UrlEncode(text);

                        string val = (text != null) ? (text + "=") : string.Empty;
                        string[] vals = httpValueCollection.GetValues(i);

                        if (sb.Length > 0)
                            sb.Append('&');

                        if (vals == null || vals.Length == 0)
                            sb.Append(val);
                        else
                        {
                            if (vals.Length == 1)
                            {
                                sb.Append(val);
                                sb.Append(HttpUtility.UrlEncode(vals[0]));
                            }
                            else
                            {
                                for (int j = 0; j < vals.Length; j++)
                                {
                                    if (j > 0)
                                        sb.Append('&');

                                    sb.Append(val);
                                    sb.Append(HttpUtility.UrlEncode(vals[j]));
                                }
                            }
                        }
                    }
                }

                ub.Query = sb.ToString();
            }

            return ub.Uri;
        }

        public static string ToQueryString(this IDictionary<string, string> parameters)
        {
            using (var content = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>()))
            {
                return content.ReadAsStringAsync().Result.ToString();
            }
        }

        public static string AddQueryString(this string routeName, IDictionary<string, string> parameters) 
        {
            if (parameters == null || parameters.Count == 0)
                return routeName;
            return $"{routeName}?{parameters.ToQueryString()}";
        }

        public static Dictionary<string, string> PropertiesToDictionary(this object obj)
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null).ToString());
        }

        public static string AddQueryString(this string routeName, object parameters) 
        {
            if (parameters == null)
                return routeName;
            return $"{routeName}?{parameters.PropertiesToDictionary().ToQueryString()}";
        }

        public static string ToUrlWithQueryString(this string rootUri, string queryString)
        {
            if (string.IsNullOrWhiteSpace(rootUri))
            {
                return string.Empty;
            }
            UriBuilder builder = new UriBuilder(rootUri)
            {
                Query = queryString
            };
            return builder.ToString();
        }

        public static NameValueCollection ToNameValueCollection<TKey, TValue>(
           this IDictionary<TKey, TValue> dict)
        {
            var nameValueCollection = new NameValueCollection();

            foreach (var kvp in dict)
            {
                string value = null;
                if (kvp.Value != null)
                    value = kvp.Value.ToString();

                nameValueCollection.Add(kvp.Key.ToString(), value);
            }

            return nameValueCollection;
        }

        public static T DictionaryToObject<T>(this IDictionary<string, string> dict) where T : new()
        {
            var t = new T();
            PropertyInfo[] properties = t.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                KeyValuePair<string, string> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                // Find which property type (int, string, double? etc) the CURRENT property is...
                Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

                // Fix nullables...
                Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

                // ...and change the type
                object newA = Convert.ChangeType(item.Value, newT);
                t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
            }
            return t;
        }

        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return guid == null || guid == Guid.Empty;
        }

        public static bool IsNullOrEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static int ToInt(this Enum value)
        {
            return (int)((object)value);
        }

        public static T ToEnum<T>(this int value) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new TypeAccessException("Type must be an Enum.");
            }

            return (T)Enum.ToObject(typeof(T), value);
        }

        public static string ToBitsString(this byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }
    }
}
