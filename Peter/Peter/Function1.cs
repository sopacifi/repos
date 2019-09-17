using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

// object that we expect to read from the log file
class LogEntry
{
    public string process = "";
    public string msg = "";
    public string file = "";
    public string severity = "";
    public string environment = "";
    public string time = "";
    public string type = "";
}


namespace Peter
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([BlobTrigger("samples-workitems/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            // log trigger
            log.LogInformation($"Blob trigger processing blob => Name:{name} ; Size:{myBlob.Length} Bytes");

            // read the blob
            byte[] raw = new byte[myBlob.Length];
            int readResult = myBlob.Read(raw, 0, (int)myBlob.Length);
            string content = System.Text.Encoding.Default.GetString(raw);

            // split blob into lines
            string[] logEntries = content.Split('\n');

            EventId eventId = new EventId(30460, "EDGAR Data Quality Violation");

        }
    }
}
