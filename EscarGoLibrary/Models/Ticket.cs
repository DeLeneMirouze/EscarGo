namespace EscarGoLibrary.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public Visiteur Acheteur { get; set; }
        public int NbPlaces { get; set; }
    }
}
