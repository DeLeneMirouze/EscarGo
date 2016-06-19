#region using
using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.Repositories
{
    public sealed class TicketRepository: BaseDataRepository, ITicketRepository
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
        public bool AddTicket(int courseId, int visiteurId, int nbPlaces)
        {
            // enregistre la demande d'achat
            Ticket ticket = new Ticket();
            ticket.AcheteurId = visiteurId;
            ticket.CourseId = courseId;
            ticket.NbPlaces = nbPlaces;
            ticket.DateAchat = DateTime.Now;

            Context.Tickets.Add(ticket);
            Context.SaveChanges();

            // confirme la demande
            Course course = Context.Courses.First(c => c.CourseId == courseId);
            if (course == null || course.NbTickets < nbPlaces)
            {
                return false;
            }

            course.NbTickets -= nbPlaces;
            Context.SaveChanges();

            return true;
        }

        public async Task<bool> AddTicketAsync(int courseId, int visiteurId, int nbPlaces)
        {
            // enregistre la demande d'achat
            Ticket ticket = new Ticket();
            ticket.AcheteurId = visiteurId;
            ticket.CourseId = courseId;
            ticket.NbPlaces = nbPlaces;
            ticket.DateAchat = DateTime.Now;

            Context.Tickets.Add(ticket);
            await Context.SaveChangesAsync();

            // confirme la demande
            Course course = await Context.Courses.FirstAsync(c => c.CourseId == courseId);
            if (course == null || course.NbTickets < nbPlaces)
            {
                return false;
            }

            course.NbTickets -= nbPlaces;
            await Context.SaveChangesAsync();

            return true;
        }
        #endregion
    }
}
