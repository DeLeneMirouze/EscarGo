using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;

//http://blog.amitapple.com/post/2015/06/scheduling-azure-webjobs/#.V5TxvriLShc
// http://stackoverflow.com/questions/36610952/azure-webjobs-vs-azure-functions-how-to-choose

namespace CQRSJobs
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var host = new JobHost();





            Task callTask = host.CallAsync(typeof(Functions).GetMethod("ProcessRaces"));
            Console.WriteLine("Waiting for ReturnBonusReminder async Task");

            callTask.Wait();
            Console.WriteLine("ReturnBonusReminder Task complete with status : {0}", callTask.Status);






            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}



