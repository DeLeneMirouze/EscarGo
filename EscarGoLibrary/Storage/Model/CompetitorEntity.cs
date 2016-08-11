using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Diagnostics;

namespace EscarGoLibrary.Storage.Model
{
    [DebuggerDisplay("{PartitionKey}/{RowKey}")]
    public class CompetitorEntity : TableEntity
    {
        public string Nom { get; set; }
        public int Victoires { get; set; }
        public int Defaites { get; set; }
        public string Entraineur { get; set; }
        public double SC { get; set; }
        public string Course { get; set; }
        public DateTime Date { get; set; }

    }
}
