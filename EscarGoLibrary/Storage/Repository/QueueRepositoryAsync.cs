#region using
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Configuration;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.Storage.Repository
{
    public class QueueRepositoryAsync : IQueueRepositoryAsync
    {
        #region Constructeur
        private readonly string _storageConnectionString;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudQueue _ticketQueue;

        public QueueRepositoryAsync()
        {
            _storageConnectionString = ConfigurationManager.AppSettings["storageCnx"];
            _storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            _ticketQueue = GetQueue("ticketsales");
        }
        #endregion

        #region AddMessageAsync
        public async Task AddMessageAsync(string message)
        {
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            await _ticketQueue.AddMessageAsync(queueMessage);
        }
        #endregion

        #region ReadMessageAsync
        public async Task<string> ReadMessageAsync()
        {
            CloudQueueMessage queueMessage = await _ticketQueue.GetMessageAsync();
            if (queueMessage == null)
            {
                return null;
            }

            await DeleteMessageAsync(queueMessage);

            return queueMessage.AsString;
        }
        #endregion

        #region DeleteMessageAsync
        public async Task DeleteMessageAsync(CloudQueueMessage message)
        {
            await _ticketQueue.DeleteMessageAsync(message);
        }
        #endregion

        #region GetQueue (private)
        private CloudQueue GetQueue(string name)
        {
            CloudQueueClient queueClient = _storageAccount.CreateCloudQueueClient();
            queueClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromMilliseconds(500), 3);

            CloudQueue queue = queueClient.GetQueueReference(name);
            bool retour = queue.CreateIfNotExists();

            return queue;
        }
        #endregion
    }
}
