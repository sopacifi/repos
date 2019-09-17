using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.WebJobs;

namespace WebJobTest
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        /*public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            log.WriteLine(message);
        }*/
        
        public static void AzureWebJob(TextWriter logger)
        {
            int proceesId = System.Diagnostics.Process.GetCurrentProcess().Id;
            logger.WriteLine($"api-app");
            logger.WriteLine($"Start webJob: processID {proceesId} Time {DateTime.Now.ToString()}");
            for (int i = 0; i < 10; i++)
            {
                logger.WriteLine($"Running proceedID: {proceesId} {i}");
                Thread.Sleep(1 * 60 * 1000);
            }
            logger.WriteLine("Finish WebJob");
        }
    }
}
