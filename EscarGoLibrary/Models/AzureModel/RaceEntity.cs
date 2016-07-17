using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace EscarGoLibrary.Models.AzureModel
{
    public class RaceEntity: TableEntity
    {
        public string Label { get; set; }
        public DateTime Date { get; set; }
        public string Pays { get; set; }
        public string Ville { get; set; }
        public int Likes { get; set; }
        public int ConcurrentId { get; set; }
        public String Concurrent { get; set; }
        public double SC { get; set; }
    }
}
