using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace FunctionAppChad
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,"get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {

            return await TestFreedomResponse();

        }

        public static async Task<HttpResponseMessage> TestFreedomResponse()
        {
            var contents = new FormUrlEncodedContent(new Dictionary<string, string> {
                {"Postcode", "SK151BN"},
                {"WebReference", "1589-485046-8389"}
            });
            //var proxy = new Uri("https://functionappchad.azurewebsites.net/v1");
            
            var uri = new Uri("https://preprod.theinsurers.co.uk:207/car/freedom/car/RetrieveQuotePOST");

            var user = "broker@ice.local";
            var pwd = "P4rtn3r@";
            var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(
                String.Format("{0}:{1}", user, pwd)));

            var credentials = new NetworkCredential(user, pwd);

            var handler = new HttpClientHandler { PreAuthenticate = true, Credentials = credentials };
            //var cookie = "visibility_stats = 250 - 1559086215705; webRef = 1589 - 485046 - 8389; ASP.NET_SessionId = uehpbgxlcc4ntr3tm4tcjqv4";


            var client = new HttpClient(handler);

            // Create a 'HttpWebRequest' object.
            //HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            //myHttpWebRequest.Referer = "https://preprod.theinsurers.co.uk:207/car/freedom/car/RetrieveQuotePOST";
            //myHttpWebRequest.Host = "preprod.theinsurers.co.uk:207";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
            client.DefaultRequestHeaders.Add("Host", "preprod.theinsurers.co.uk:207");
            //client.DefaultRequestHeaders.Add("Postcode", "SK151BN");
            var resp = await client.PostAsync(uri, contents);

            //return new HttpResponseMessage(HttpStatusCode.Redirect) { Content = resp.Content};

            //return new HttpResponseMessage(HttpStatusCode.Redirect) { Content = resp.Headers.};
            return resp;

        }
    }
}
