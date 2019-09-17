using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;

namespace EventHubTest
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([EventHubTrigger("samples-workitems", Connection = "Endpoint=sb://eventhubjavascript.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=fXbbHXzbLq12aIBZdd6B7Ibv7iwiXkAMC5AuZpkE9rA=")]string myEventHubMessage, TraceWriter log)
        {
            log.Info($"C# Event Hub trigger function processed a message: {myEventHubMessage}");
        }
    }
}
