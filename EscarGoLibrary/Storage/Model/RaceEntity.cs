using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace EscarGoLibrary.Storage.Model
{
    [Serializable]
    public class RaceEntity : TableEntity
    {
        public string Label { get; set; }
        public DateTime Date { get; set; }
        public string Pays { get; set; }
        public string Ville { get; set; }
        public int Likes { get; set; }
        public String Concurrent { get; set; }
        public double SC { get; set; }
        public int NbTickets { get; set; }
    }
}
