using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EscarGo.Models
{
    [DebuggerDisplay("Label")]
    public class Course
    {
        public Course()
        {
            Concurrents = new List<Concurrent>();
        }
        [Key]
        public int IdCourse { get; set; }
        public string Label { get; set; }
        public DateTime Date { get; set; }
        public string Pays { get; set; }
        public string Ville { get; set; }

        public ICollection<Concurrent> Concurrents { get; set; }
    }
}
