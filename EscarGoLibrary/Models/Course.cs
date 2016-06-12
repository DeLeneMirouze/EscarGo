using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace EscarGo.Models
{
    [DebuggerDisplay("{Label}")]
    public class Course
    {
        public Course()
        {
            Concurrents = new List<Concurrent>();
        }
        [Key]
        public int IdCourse { get; set; }
        public string Label { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Pays { get; set; }
        public string Ville { get; set; }
        [NotMapped]
        public double SC { get; set; }

        public int Likes { get; set; }
        public int NbTickets { get; set; }

        public ICollection<Concurrent> Concurrents { get; set; }
    }
}
