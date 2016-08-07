using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;

namespace EscarGoLibrary.Storage.Repository
{
    public interface IQueueRepositoryAsync
    {
        Task AddMessageAsync(string message);
        Task<string> ReadMessageAsync();
        Task DeleteMessageAsync(CloudQueueMessage message);
    }
}