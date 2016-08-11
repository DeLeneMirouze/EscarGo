using System;

namespace EscarGoLibrary.Models
{
    [Serializable]
    public class Ticket
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public Visiteur Acheteur { get; set; }
        public int AcheteurId { get; set; }
        public DateTime DateAchat { get; set; }
        public int NbPlaces { get; set; }
        public bool EstConfirme { get; set; }
    }
}
