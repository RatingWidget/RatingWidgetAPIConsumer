using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RatingWidgetAPIConsumer
{
    public class Helper
    {
        public const string ISO8601BasicFormat = "ddd, dd MMM yyyy HH:mm:ss +0000";

        public const string publicKey = "HERE GOES YOUR PUBLIC KEY";
        public const string secretKey = "HERE GOES YOUR SECRET KEY";
        public const string RWId = "HERE GOES YOUR ID";

        public static string CallRWAPI(string url, string method, string queryParams = "")
        {
            var requestDateTime = DateTime.UtcNow;
            var dateTimeStamp = requestDateTime.ToString(ISO8601BasicFormat, CultureInfo.InvariantCulture);

            var StringToSign = method + "\n\napplication/json\n" + dateTimeStamp + "\n" + url;

            var signature = CreateToken(secretKey, StringToSign);

            var authHeader = "RW " + RWId + ":" + publicKey + ":" + signature;

            var req = HttpWebRequest.Create("http://api.rating-widget.com" + url + queryParams);

            req.ContentType = "application/json";

            req.Method = method;

            MethodInfo priMethod = req.Headers.GetType().GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);
            priMethod.Invoke(req.Headers, new[] { "Date", dateTimeStamp });
            req.Headers.Add("Authorization", authHeader);

            var response = req.GetResponse();
            var jsonResponse = "";
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                jsonResponse = sr.ReadToEnd();
            }

            return jsonResponse;
        }

        public static string CreateToken(string key, string request)
        {
            var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(request));
            var hashstring = "";
            foreach (byte test in hmacsha256.Hash)
            {
                hashstring += test.ToString("X2");
            }

            string hashed = Convert.ToBase64String(Encoding.UTF8.GetBytes(hashstring.ToLower()));


            return Base64ForUrlEncode(hashed);
        }

        private static string Base64ForUrlEncode(string str)
        {
            return str.Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}
