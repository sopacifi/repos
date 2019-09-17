using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace com.honcho.veh.redirect
{
    public class SimplifiedCompleter
    {


        private static HttpClient client = new HttpClient();
        public async Task<HttpResponseMessage> HandleSimplifiedCompletion(HttpRequest req)
        {

            // We need this if we want to use the Authority
            var url = "https://development.theinsurers.co.uk:207/car/freedom/car/RetrieveQuotePOST";
            var authority = new Uri(url);
            var isPost = true;
            byte[] byteArray = null;

            var user = Environment.GetEnvironmentVariable("freedomUser") ?? "broker@ice.local";
            var pwd = Environment.GetEnvironmentVariable("freedomPwd") ?? "P4rtn3r@";
            var credentials = new NetworkCredential(user, pwd /*, authority.Authority*/);

            byteArray = Encoding.ASCII.GetBytes($"{user}:{pwd}");
            Console.WriteLine($"{Environment.NewLine}Authorization: {Encoding.ASCII.GetString(byteArray)}{Environment.NewLine}{Convert.ToBase64String(byteArray)}{Environment.NewLine}");


            var contents = new FormUrlEncodedContent(new Dictionary<string, string> {
                {"Postcode", "SK151BN"},
                {"WebReference", "1589-485046-8389"}});

            var proxy = new Uri("https://honcho-veh-completion-api.azurewebsites.net/V999");
            var handler = new HttpClientHandler { Credentials = credentials, AllowAutoRedirect = false };
            //var resp = isPost ? await client.PostAsync(proxy, contents) : await client.GetAsync($"{proxy}?{await contents.ReadAsStringAsync()}");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YnJva2VyQGljZS5sb2NhbDpQNHJ0bjNyQA==");
            var retVal = await client.PostAsync(proxy, contents);

            return retVal;
        }
    }
}
