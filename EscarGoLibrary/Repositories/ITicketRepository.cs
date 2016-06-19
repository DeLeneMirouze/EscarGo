using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public interface ITicketRepository
    {
        List<Visiteur> GetVisiteurs();
        bool AddTicket(int courseId, int visiteurId, int nbPlace);

        Task<List<Visiteur>> GetVisiteursAsync();
        Task<bool> AddTicketAsync(int courseId, int visiteurId, int nbPlace);
    }
}
