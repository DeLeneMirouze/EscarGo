#region using
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Configuration;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.Storage.Repository
{
    public class QueueRepository : IQueueRepositoryAsync
    {
        #region Constructeur
        private readonly string _storageConnectionString;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudQueue _ticketQueue;

        public QueueRepository()
        {
            _storageConnectionString = ConfigurationManager.AppSettings["storageCnx"];
            _storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            _ticketQueue = GetQueue("ticketSales");
        }
        #endregion

        #region AddMessage
        public async Task AddMessageAsync(string message)
        {
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            await _ticketQueue.AddMessageAsync(queueMessage);
        }
        #endregion

        #region ReadMessage
        public async Task<string> ReadMessageAsync()
        {
            CloudQueueMessage queueMessage = await _ticketQueue.GetMessageAsync();
            return queueMessage.AsString;
        } 
        #endregion

        #region GetQueue (private)
        private CloudQueue GetQueue(string name)
        {
            var queueClient = _storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(name);
            queue.CreateIfNotExistsAsync();

            return queue;
        }
        #endregion
    }
}
