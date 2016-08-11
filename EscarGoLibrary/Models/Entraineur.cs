using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EscarGoLibrary.Models
{
    [DebuggerDisplay("{Nom}")]
    [Serializable]
    public class Entraineur
    {
        [Key]
        public int EntraineurId { get; set; }
        public string Nom { get; set; }
    }
}
