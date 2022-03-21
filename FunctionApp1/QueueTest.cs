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
using System.Configuration;
using Microsoft.Extensions.Configuration;

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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .Build();
            var connectionString = configuration["Values:StorageForQueue"];
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
