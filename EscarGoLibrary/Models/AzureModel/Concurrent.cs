using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscarGoLibrary.Models.AzureModel
{
    public class Concurrent: TableEntity
    {
        public string Nom { get; set; }
        public int Victoires { get; set; }
        public int Defaites { get; set; }
        public string Entraineur { get; set; }
        public DateTime DateDernierPari { get; set; }
        public double SC { get; set; }

        public int IdConcurrent { get; set; }
        public int IdCourse { get; set; }
        public int IdPari { get; set; }
    }
}
