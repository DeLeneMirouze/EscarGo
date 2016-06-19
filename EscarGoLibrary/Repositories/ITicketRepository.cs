using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.Repositories
{
    public interface ITicketRepository
    {
        List<Visiteur> GetVisiteurs();
        Ticket AddTicket(int courseId, int visiteurId, int nbPlace);
    }
}
