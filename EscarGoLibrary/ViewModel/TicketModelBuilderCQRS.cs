#region using
using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories.CQRS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
#endregion

namespace EscarGoLibrary.ViewModel
{
    public class TicketModelBuilderCQRS
    {
        #region Constructeur
        readonly IUnitOfWorkCQRS _unitOfWork;


        public TicketModelBuilderCQRS(IUnitOfWorkCQRS unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        //#region GetTicketAsync
        //public async Task<BuyTicketViewModel> GetTicketAsync(int courseId)
        //{
        //    BuyTicketViewModel vm = new BuyTicketViewModel();

        //    List<Visiteur> visiteurs = await _unitOfWork.TicketRepositoryAsync.GetVisiteursAsync();
        //    vm.Acheteurs = new SelectList(visiteurs, "Id", "Nom");
        //    vm.Course = await _unitOfWork.RaceRepository.GetCourseByIdAsync(courseId);
        //    vm.NbPlaces = 1;

        //    return vm;
        //}
        //#endregion

        #region PostTicketAsync
        public async Task<ConfirmationAchatViewModel> PostTicketAsync(BuyTicketViewModel buyTicketViewModel)
        {
            ConfirmationAchatViewModel vm = new ConfirmationAchatViewModel();
            Ticket ticket = null;

            try
            {
                ticket = await _unitOfWork.TicketRepositoryAsync.AddTicketAsync(buyTicketViewModel.Course.CourseId, buyTicketViewModel.AcheteurSelectionne, buyTicketViewModel.NbPlaces);
                await _unitOfWork.SaveAsync();

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
