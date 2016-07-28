using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.Async
{
    public interface ITicketRepositoryAsync
    {
        Task<List<Visiteur>> GetVisiteursAsync();
        Task<Ticket> AddTicketAsync(int courseId, int visiteurId, int nbPlace);
    }
}
