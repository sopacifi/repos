using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;


[FunctionName("SimplfiedCompletion")]
public static async Task<HttpResponseMessage> SimplfiedCompletion(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
        HttpRequest req, ILogger log, ExecutionContext context)
{

    log.LogInformation($"Re-directing (version: {_version}) completion handler");

    var completer = new SimplifiedCompleter();

    return await completer.HandleSimplifiedCompletion(req);

}