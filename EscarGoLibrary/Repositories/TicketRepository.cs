#region using
using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return SqlAzureRetry.ExecuteAction(() => Context.Visiteurs.OrderBy(v => v.Nom).ToList());
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
            Course course = SqlAzureRetry.ExecuteAction(() => Context.Courses.First(c => c.CourseId == courseId));
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
