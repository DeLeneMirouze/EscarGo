#region using
using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories.Async;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.Repositories
{
    public sealed class TicketRepository: BaseDataRepository, ITicketRepository, ITicketRepositoryAsync
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

        #region AddTicket
        public Ticket AddTicket(int courseId, int visiteurId, int nbPlaces)
        {
            // enregistre la demande d'achat
            Ticket ticket = new Ticket();
            ticket.AcheteurId = visiteurId;
            ticket.CourseId = courseId;
            ticket.NbPlaces = nbPlaces;
            ticket.DateAchat = DateTime.Now;

            Context.Tickets.Add(ticket);

            // confirme la demande
            Course course = Context.Courses.First(c => c.CourseId == courseId);
            if (course == null || course.NbTickets < nbPlaces)
            {
                return null;
            }

            course.NbTickets -= nbPlaces;

            return ticket;
        }

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
            Course course = await Context.Courses.FirstAsync(c => c.CourseId == courseId);
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
