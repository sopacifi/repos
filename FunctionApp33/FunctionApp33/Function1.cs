using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FunctionApp33
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            // Grab connection string

            var strConn = Environment.GetEnvironmentVariable("rNetwork_FuncTest");
            using (SqlConnection objConn = new SqlConnection(strConn))
            {
                // Connection object
                objConn.Open();
                // Command text

                var text = "CREATE Table.Example ";

                using (SqlCommand cmd = new SqlCommand(text, objConn))
                {
                    // Execute the command and log the # rows affected.
                    var rows = await cmd.ExecuteNonQueryAsync();
                    log.LogInformation($"{rows} rows were updated");
                }


            }
        }
    }
}
