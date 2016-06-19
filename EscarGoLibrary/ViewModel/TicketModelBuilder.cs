using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EscarGoLibrary.ViewModel
{
    public class TicketModelBuilder
    {
        #region Constructeur
        readonly ITicketRepository _ticketRepository;
        readonly ICourseRepository _courseRepository;

        public TicketModelBuilder(ITicketRepository ticketRepository, ICourseRepository courseRepository)
        {
            _ticketRepository = ticketRepository;
            _courseRepository = courseRepository;
        }
        #endregion

        #region GetTicket
        public BuyTicketViewModel GetTicket(int courseId)
        {
            BuyTicketViewModel vm = new BuyTicketViewModel();

            List<Visiteur> visiteurs = _ticketRepository.GetVisiteurs();
            vm.Acheteurs = new SelectList(visiteurs, "Id", "Nom");
            vm.Course = _courseRepository.GetCourseById(courseId);
            vm.NbPlaces = 1;

            return vm;
        }
        #endregion

        #region PostTicket
        public ConfirmationAchatViewModel PostTicket(BuyTicketViewModel buyTicketViewModel)
        {
            ConfirmationAchatViewModel vm = new ConfirmationAchatViewModel();
            Ticket ticket = null;

            try
            {
                ticket = _ticketRepository.AddTicket(buyTicketViewModel.Course.CourseId, buyTicketViewModel.AcheteurSelectionne, buyTicketViewModel.NbPlaces);

                vm.EstEnregistre = ticket != null;

                vm.Course = buyTicketViewModel.Course;
            }
            catch (Exception)
            {
                vm.EstEnregistre = false;
            }

            if (vm.EstEnregistre)
            {
                vm.Message = "Nous avons pré-enregistré votre achat. Vous devez attendre sa confirmation par email pour qu'il soit définitif";
                vm.NbTickets = ticket.NbPlaces;
            }
            else
            {
                vm.Message = "Il n'a pas été possible d'enregistrer votre achat";
            }
            return vm;
        } 
        #endregion
    }
}
