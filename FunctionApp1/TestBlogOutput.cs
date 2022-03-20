using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class TestBlogOutput
    {
        [FunctionName("TestBlogOutput")]
        [StorageAccount("learn2f9affd1fdc1419765")]
        public void Run(
            [TimerTrigger("* */5 * * * *")] TimerInfo myTimer,
            [Blob("test-samples-output/test-output.txt")] out string myOutputBlob,
            [Blob("test-samples-output/test-output.txt")] in string currentBlog,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            myOutputBlob = currentBlog + "Test Text";
        }
    }
}
