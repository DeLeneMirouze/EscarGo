using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EscarGoLibrary.Models
{
    [DebuggerDisplay("{SC}")]
    [Serializable]
    public class Pari
    {
        [Key]
        public int PariId { get; set; }
        public DateTime DateDernierPari { get; set; }
        public int NbParis { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int ConcurrentId { get; set; }
        public Concurrent Concurrent { get; set; }
        /// <summary>
        /// Côte simple gagnant
        /// </summary>
        public double SC { get; set; }
    }
}
