using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp25
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("myqueue", Connection = "DefaultEndpointsProtocol=https;AccountName=azurefunctionsp99ef;AccountKey=LOvxwZGX0HtnQVlt05Ot7OC7qEWfbTu20mP7Q0RjAwAZiy9NcnFZZMvsUKPqrrGlI0PWe5O01Sk66kSXsQDLLw==;EndpointSuffix=core.windows.net")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
