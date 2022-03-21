using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Queues;

namespace MyDemoFunctions
{
    public static class QueueTest
    {
        [FunctionName("QueueTest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            var connectionString = "DefaultEndpointsProtocol=https;AccountName=cloudshell1401215711;AccountKey=kvSHoFpBzd2iVBcnDpppxYDv774VCE0l4u0o08Jyiqi9rcJXKC0HC/LHc+HHEU9JRU37yZkF3FkaSafEi8HHBA==;EndpointSuffix=core.windows.net";
            var queueName = "zek-queue";
            var client = new QueueClient(connectionString, queueName);
            
            var messageToSend = "Hello";
            log.LogInformation($"Sending message : {messageToSend}");
            await client.SendMessageAsync(messageToSend);
            
            log.LogInformation($"Now we are fetching the message");
            var messageReceived = await client.PeekMessageAsync();
            
            log.LogInformation($"We have received '{messageReceived.Value?.MessageText}'");

            return new OkObjectResult(default);
        }
    }
}
