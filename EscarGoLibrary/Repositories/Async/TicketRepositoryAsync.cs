#region using
using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.Repositories.Async
{
    public class TicketRepositoryAsync: BaseDataRepository,  ITicketRepositoryAsync
    {
        #region Constructeur
        public TicketRepositoryAsync(EscarGoContext context) : base(context)
        {

        }
        #endregion

        #region GetVisiteurs
        public async Task<List<Visiteur>> GetVisiteursAsync()
        {
            return await SqlAzureRetry.ExecuteAsync(async () => await Context.Visiteurs.OrderBy(v => v.Nom).ToListAsync());
        }
        #endregion

        #region AddTicket
        public async Task<Ticket> AddTicketAsync(int courseId, int visiteurId, int nbPlaces)
        {
            // enregistre la demande d'achat
            Ticket ticket = new Ticket();
            ticket.AcheteurId = visiteurId;
            ticket.CourseId = courseId;
            ticket.NbPlaces = nbPlaces;
            ticket.DateAchat = DateTime.Now;

            Context.Tickets.Add(ticket);

            // confirme la demande
            Course course = await SqlAzureRetry.ExecuteAsync(async () => await Context.Courses.FirstAsync(c => c.CourseId == courseId));
            if (course == null || course.NbTickets < nbPlaces)
            {
                return null;
            }

            course.NbTickets -= nbPlaces;

            return ticket;
        }
        #endregion
    }
}
