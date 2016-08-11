using System;
using System.Diagnostics;

namespace EscarGoLibrary.Models
{
    [DebuggerDisplay("{Nom}")]
    [Serializable]
    public class Visiteur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }
}
