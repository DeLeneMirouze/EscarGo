using System.Diagnostics;

namespace EscarGoLibrary.Models
{
    [DebuggerDisplay("{Nom}")]
    public class Visiteur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }
}
