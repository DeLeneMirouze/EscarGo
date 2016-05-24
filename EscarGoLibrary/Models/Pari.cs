using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EscarGo.Models
{
    [DebuggerDisplay("SC")]
    public class Pari
    {
        [Key]
        public int IdPari { get; set; }

        ///// <summary>
        ///// Côte simple gagnant
        ///// </summary>
        //public double SC { get; set; }
        public DateTime DateDernierPari { get; set; }
        public int NbParis { get; set; }

        public int IdCourse { get; set; }
        public Course Course { get; set; }
        public int IdConcurrent { get; set; }
        public Concurrent Concurrent { get; set; }
    }
}
