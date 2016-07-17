#region using
using EscarGoLibrary.Models;
using EscarGoLibrary.Models.AzureModel;
using EscarGoLibrary.ViewModel.AzureModel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
#endregion

namespace EscarGoLibrary.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        #region Constructeur
        private readonly string _storageConnectionString;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTable _competitorTable;

        public StorageRepository()
        {
            _storageConnectionString = ConfigurationManager.AppSettings["storageCnx"];
            _storageAccount = CloudStorageAccount.Parse(_storageConnectionString);

            _competitorTable = GetTable("Competitors");
        }
        #endregion

        #region GetCompetitors
        public List<Concurrent> GetCompetitors(string competitorId)
        {
            List<Concurrent> concurrents = new List<Concurrent>();

            TableQuery<CompetitorEntity> query = new TableQuery<CompetitorEntity>();

            var result = _competitorTable.ExecuteQuery(query);
            foreach (CompetitorEntity nosql in result)
            {
                var concurrent = nosql.ToConcurrent();
                concurrents.Add(concurrent);
            }
            return concurrents.OrderBy(c => c.Nom).ToList();
        }
        #endregion

        #region GetCourses
        public List<Course> GetCourses(string competitorId)
        {
            List<Course> concurrents = new List<Course>();

            TableQuery<RaceEntity> query = new TableQuery<RaceEntity>();

            var result = _competitorTable.ExecuteQuery(query);
            foreach (RaceEntity nosql in result)
            {
                if (nosql.Date < DateTime.Now)
                {
                    continue;
                }

                Course course = nosql.ToCourse();
                concurrents.Add(course);
            }
            return concurrents
                .OrderBy(c => c.Label).ToList();
        }
        #endregion

        #region GetTable (private)
        private CloudTable GetTable(string name)
        {
            var tableClient = _storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(name);
            table.CreateIfNotExistsAsync();

            return table;
        }
        #endregion
    }
}
