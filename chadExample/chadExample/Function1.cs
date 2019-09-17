using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using com.honcho.aws.communicator;
using com.honcho.azure.utils;
using com.honcho.constants;
using com.honcho.exceptions;
using com.honcho.veh.repository;
using com.honcho.veh.repository.entities;
using com.honcho.vehicle.risk.models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;


[FunctionName("Function1")]
public static async Task<HttpResponseMessage> Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
    ILogger log, ExecutionContext context)
{
    log.LogInformation("Strat");

    var completer = new TestFreedomResponse();
}
