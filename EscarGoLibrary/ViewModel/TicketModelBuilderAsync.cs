using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories.Async;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EscarGoLibrary.ViewModel
{
    public sealed class TicketModelBuilderAsync
    {
        #region Constructeur
        readonly IUnitOfWorkAsync _unitOfWorkAsync;


        public TicketModelBuilderAsync(IUnitOfWorkAsync unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        #endregion

        #region GetTicketAsync
        public async Task<BuyTicketViewModel> GetTicketAsync(int courseId)
        {
            BuyTicketViewModel vm = new BuyTicketViewModel();

            List<Visiteur> visiteurs = await _unitOfWorkAsync.TicketRepositoryAsync.GetVisiteursAsync();
            vm.Acheteurs = new SelectList(visiteurs, "Id", "Nom");
            vm.Course =await _unitOfWorkAsync.RaceRepositoryAsync.GetCourseByIdAsync(courseId);
            vm.NbPlaces = 1;

            return vm;
        }
        #endregion

        #region PostTicketAsync
        public async Task<ConfirmationAchatViewModel> PostTicketAsync(BuyTicketViewModel buyTicketViewModel)
        {
            ConfirmationAchatViewModel vm = new ConfirmationAchatViewModel();
            vm.DateAchat = DateTime.Now;
            Ticket ticket = null;

            try
            {
                ticket = await _unitOfWorkAsync.TicketRepositoryAsync.AddTicketAsync(buyTicketViewModel.Course.CourseId, buyTicketViewModel.AcheteurSelectionne, buyTicketViewModel.NbPlaces);
                await _unitOfWorkAsync.SaveAsync();

                vm.EstEnregistre = (ticket != null);

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
