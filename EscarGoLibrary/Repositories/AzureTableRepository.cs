using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public sealed class AzureTableRepository: IAzureTableRepository
    {
        #region Constructeur
        CloudTableClient _tableClient;

        public AzureTableRepository()
        {
            CloudStorageAccount csa = CloudStorageAccount.Parse(_storageConnectionString);
            _tableClient = csa.CreateCloudTableClient();
        } 
        #endregion

        #region GetTable (private)
        private async Task<CloudTable> GetTable(string tableName)
        {
            CloudTable table = _tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }
        #endregion


        string _storageConnectionString = 
            "";
    }
}
