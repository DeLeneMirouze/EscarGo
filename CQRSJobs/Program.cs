using Microsoft.Azure.WebJobs;

//http://blog.amitapple.com/post/2015/06/scheduling-azure-webjobs/#.V5TxvriLShc
// http://stackoverflow.com/questions/36610952/azure-webjobs-vs-azure-functions-how-to-choose
//http://stackoverflow.com/questions/24486765/scheduled-azure-webjob-but-noautomatictrigger-method-not-invoked
//http://cronexpressiondescriptor.azurewebsites.net/?Language=fr&DayOfWeekStartIndexOne=false&Use24HourFormat=false&VerboseDescription=false&Expression=*%2F2+*+*+*+*+
//http://www.thesitewizard.com/general/set-cron-job.shtml
//http://stackoverflow.com/questions/26110998/azure-webjobs-no-functions-found-how-do-i-make-a-trigger-less-job?rq=1
//https://azure.microsoft.com/en-us/documentation/articles/web-sites-create-web-jobs/
//http://stackoverflow.com/questions/34030441/scheduled-webjob
//http://blog.amitapple.com/post/2015/06/scheduling-azure-webjobs/#.V5ohdI9OJQK

namespace CQRSJobs
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

            host.Call(typeof(Functions).GetMethod("ProcessRaces"));
            host.Call(typeof(Functions).GetMethod("ProcessCompetitors"));

            // The following code ensures that the WebJob will be running continuously
            //host.RunAndBlock();
        }
    }
}



