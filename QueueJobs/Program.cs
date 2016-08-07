﻿using Microsoft.Azure.WebJobs;

namespace QueueJobs
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            JobHostConfiguration config = new JobHostConfiguration();
            config.UseTimers();
            var host = new JobHost(config);

            host.Call(typeof(Functions).GetMethod("ProcessTicketSales"));

            // The following code ensures that the WebJob will be running continuously
            //host.RunAndBlock();
        }
    }
}
