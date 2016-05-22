using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EscarGo.Models
{
    public class Concurrent
    {
        public Concurrent()
        {
            Courses = new List<Course>();
        }
        public string Nom { get; set; }
        [Key]
        public int IdConcurrent { get; set; }
        public int Victoires { get; set; }
        public int Defaites { get; set; }
        public string Entraineur { get; set; }
        public Pari Pari { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
