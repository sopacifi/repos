using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace com.honcho.veh.redirect
{
    public class Function2
    {
        private static HttpClient client = new HttpClient();
        public async Task<HttpResponseMessage> HandleSimplifiedCompletion(HttpRequest req)
        {
            var contents = new FormUrlEncodedContent(new Dictionary<string, string> {
                {"Postcode", "SK87JU"},
                {"WebReference",  "5740-104569-9358"}
            });
            var uri = new Uri("https://preprod.theinsurers.co.uk:207/car/freedom/car/RetrieveQuotePOST");

            client = new HttpClient(new HttpClientHandler());

            var resp = await client.PostAsync(uri, contents);

            return resp;
        }
    }
}
