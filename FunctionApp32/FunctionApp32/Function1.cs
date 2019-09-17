using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;

namespace FunctionApp32
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run(string myQueueItem, out IDictionary<string, string> notification, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
            notification = GetTemplateProperties(myQueueItem);
        }

        private static IDictionary<string, string> GetTemplateProperties(string message)
        {
            Dictionary<string, string> templateProperties = new Dictionary<string, string>();
            templateProperties["message"] = message;
            return templateProperties;
        }
    }
}
