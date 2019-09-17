using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault.Models;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;


namespace CommitterResponseAzureFunction
{
    public static class CommitterResponseAzureFunction
    {
        /// <summary>
        /// Azure function HTTP trigger to handle committer response from email sent by OrphanWorkflowRunner
        /// </summary>
        /// <param name="req"></param>
        /// <param name="componentCommitters"></param>
        /// <param name="componentName"></param>
        /// <param name="workflowName"></param>
        /// <param name="action"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("CommitterResponseAzureFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "committerresponse/{componentCommitters}/{componentName:alpha}/{workflowName:alpha}/{action:int}")] HttpRequestMessage req, string componentCommitters, string componentName, string workflowName, int action, TraceWriter log, ClaimsPrincipal claimsPrincipal)
        {
            // Format response date
            string responseDate = DateTime.Today.ToString("yyyy-MM-dd");

            // Chosen action: 0 = terminate, 1 = extend
            string actionToTake = action.Equals(0) ? "Terminate" : "Extend";

            // Email fetched from claimsPrincipal
            string AADEmail = claimsPrincipal.Identity.Name.ToString();

            // Check if ADD email alias is a valid committer for component
            string alias = AADEmail.Split('@')[0];
            string[] componentCommittersSplit = componentCommitters.Split(';');
            bool validCommiter = componentCommittersSplit.Any(alias.Contains) ? true : false;

            log.Info("HTTP trigger function processed a workflow update request." + "Component Name:" + componentName + ";  Workflow Name:" + workflowName + ";  Response Date:" + responseDate + ";  Action Choosen:" + actionToTake + ";  AAD Email:" + AADEmail + ";  Valid Committer:" + validCommiter.ToString());

            // Check parameters passed to route
            if (string.IsNullOrEmpty(workflowName) || string.IsNullOrEmpty(componentName) || string.IsNullOrEmpty(AADEmail) || string.IsNullOrEmpty(componentCommitters) || (!action.Equals(1) && !action.Equals(0)))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Status update unsuccessful; Missing or invalid parameter(s)");
            }

            // Check committer is valid for component
            if (validCommiter.Equals(false))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, string.Format("Status update unsuccessful; {0} not authorized to update workflow status for component {1}.", AADEmail, componentName));
            }

            //var connectionString = FetchConnectionStringAsync(req, log).Result;

            //  if (connectionString == null){
            //      return req.CreateErrorResponse(HttpStatusCode.Conflict, "Key vault exception");
            //  }

            // Update PipelineHealth DB with user response
            //bool sqlUpdateStatus = UpdateSangamNotificationTable(log, componentName,workflowName,alias,responseDate,action).Result;

            // Return update result and metadata
            return req.CreateResponse(HttpStatusCode.OK, "Status update successful:" + "; Component Name:" + componentName + ";  Workflow Name:" + workflowName + ";  Response Date:" + responseDate + ";  Action Taken:" + actionToTake + "; Committer:" + alias);
        }
    }
}