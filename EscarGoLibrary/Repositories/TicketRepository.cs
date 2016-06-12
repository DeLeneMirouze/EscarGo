using EscarGo.Repositories;
using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EscarGoLibrary.Repositories
{
    public class TicketRepository: BaseDataRepository, ITicketRepository
    {
        #region Constructeur
        public TicketRepository(EscarGoContext context) : base(context)
        {

        }
        #endregion

        #region GetVisiteurs
        public List<Visiteur> GetVisiteurs()
        {
            return Context.Visiteurs.OrderBy(v => v.Nom).ToList();
        }
        public async Task<List<Visiteur>> GetVisiteursAsync()
        {
            return await Context.Visiteurs.OrderBy(v => v.Nom).ToListAsync();
        } 
        #endregion
    }
}
