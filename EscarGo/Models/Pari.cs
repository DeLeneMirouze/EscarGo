using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscarGo.Models
{
    public class Pari
    {
        [Key]
        public int IdPari { get; set; }

        public double Montants { get; set; }
        public DateTime DateDernierPari { get; set; }
        public int NbParis { get; set; }
    }
}
