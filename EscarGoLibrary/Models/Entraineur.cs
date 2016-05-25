using System.ComponentModel.DataAnnotations;

namespace EscarGoLibrary.Models
{
    public class Entraineur
    {
        [Key]
        public int IdEntraineur { get; set; }
        public string Nom { get; set; }
    }
}
