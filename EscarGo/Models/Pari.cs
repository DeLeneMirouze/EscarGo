using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EscarGo.Models
{
    [DebuggerDisplay("SC")]
    public class Pari
    {
        [Key]
        public int IdPari { get; set; }

        /// <summary>
        /// Côte simple gagnant
        /// </summary>
        public double SC { get; set; }
        public DateTime DateDernierPari { get; set; }
        public int NbParis { get; set; }
    }
}
