#region using
using EscarGoLibrary.Models;
using EscarGoLibrary.Storage.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
#endregion

namespace EscarGoLibrary.Storage.Repository
{
    public class TableStorageRepository : ITableStorageRepository
    {
        #region Constructeur
        private readonly string _storageConnectionString;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTable _competitorTable;
        private readonly CloudTable _raceTable;

        public TableStorageRepository()
        {
            _storageConnectionString = ConfigurationManager.AppSettings["storageCnx"];
            _storageAccount = CloudStorageAccount.Parse(_storageConnectionString);

            _competitorTable = GetTable("Competitors");
            _raceTable = GetTable("Races");
        }
        #endregion

        #region GetCompetitors
        public List<Concurrent> GetCompetitors()
        {
            List<Concurrent> concurrents = new List<Concurrent>();

            TableQuery<CompetitorEntity> query = new TableQuery<CompetitorEntity>();
         
            // attention, il y a une limite à 1000 lignes par requêtes
            var result = _competitorTable.ExecuteQuery(query);
            foreach (CompetitorEntity nosql in result)
            {
                var concurrent = nosql.ToConcurrent();
                concurrents.Add(concurrent);
            }
            return concurrents.OrderBy(c => c.Nom).ToList();
        }
        #endregion

        #region GetCompetitorById
        public List<CompetitorEntity> GetCompetitorById(int idCompetitor)
        {
            String partitionFilter = TableQuery.GenerateFilterCondition("PartitionKey",
                QueryComparisons.Equal,
                idCompetitor.ToString());
            TableQuery<CompetitorEntity> partitionQuery = new TableQuery<CompetitorEntity>().Where(partitionFilter);

           var entities = _competitorTable.ExecuteQuery(partitionQuery);
            if (entities == null)
            {
                return null;
            }

            return entities.ToList();
        }
        #endregion

        #region SetCompetitors

        public void SetCompetitors(List<CompetitorEntity> competitors)
        {
            foreach (CompetitorEntity competitor in competitors)
            {
                TableOperation operation = TableOperation.InsertOrReplace(competitor);
                var retour = _competitorTable.Execute(operation);
            }
        }
        #endregion

        #region SetRaces
        public void SetRaces(List<RaceEntity> races)
        {
            foreach (RaceEntity race in races)
            {
                TableOperation operation = TableOperation.InsertOrReplace(race);
                var retour = _raceTable.Execute(operation);
            }
        } 
        #endregion

        #region GetRaces
        public List<Course> GetRaces()
        {
            List<Course> concurrents = new List<Course>();

            TableQuery<RaceEntity> query = new TableQuery<RaceEntity>();

            // attention, il y a une limite à 1000 lignes par requêtes
            var result = _raceTable.ExecuteQuery(query);
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

        #region GetRaceById
        public List<RaceEntity> GetRaceById(int idRace)
        {
            String partitionFilter = TableQuery.GenerateFilterCondition("PartitionKey",
                QueryComparisons.Equal,
                idRace.ToString());
            TableQuery<RaceEntity> partitionQuery = new TableQuery<RaceEntity>().Where(partitionFilter);

            var entities = _raceTable.ExecuteQuery(partitionQuery);
            if (entities == null)
            {
                return null;
            }

            return entities.ToList();
        }
        #endregion

        #region GetTable (private)
        private CloudTable GetTable(string name)
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            tableClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromMilliseconds(500), 3);

            CloudTable table = tableClient.GetTableReference(name);
            table.CreateIfNotExistsAsync();

            return table;
        }
        #endregion
    }
}
