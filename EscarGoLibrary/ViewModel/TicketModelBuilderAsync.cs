using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EscarGoLibrary.ViewModel
{
    public sealed class TicketModelBuilderAsync
    {
        #region Constructeur
        readonly ITicketRepositoryAsync _ticketRepository;
        readonly ICourseRepositoryAsync _courseRepository;

        public TicketModelBuilderAsync(ITicketRepositoryAsync ticketRepository, ICourseRepositoryAsync courseRepository)
        {
            _ticketRepository = ticketRepository;
            _courseRepository = courseRepository;
        }
        #endregion

        #region GetTicketAsync
        public async Task<BuyTicketViewModel> GetTicketAsync(int courseId)
        {
            BuyTicketViewModel vm = new BuyTicketViewModel();

            List<Visiteur> visiteurs = await _ticketRepository.GetVisiteursAsync();
            vm.Acheteurs = new SelectList(visiteurs, "Id", "Nom");
            vm.Course =await  _courseRepository.GetCourseByIdAsync(courseId);
            vm.NbPlaces = 1;

            return vm;
        }
        #endregion

        #region PostTicketAsync
        public async Task<ConfirmationAchatViewModel> PostTicketAsync(BuyTicketViewModel buyTicketViewModel)
        {
            ConfirmationAchatViewModel vm = new ConfirmationAchatViewModel();
            Ticket ticket = null;

            try
            {
                ticket = await _ticketRepository.AddTicketAsync(buyTicketViewModel.Course.CourseId, buyTicketViewModel.AcheteurSelectionne, buyTicketViewModel.NbPlaces);

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
