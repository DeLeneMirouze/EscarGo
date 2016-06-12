using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public interface ITicketRepository
    {
        List<Visiteur> GetVisiteurs();
        Task<List<Visiteur>> GetVisiteursAsync();
    }
}
