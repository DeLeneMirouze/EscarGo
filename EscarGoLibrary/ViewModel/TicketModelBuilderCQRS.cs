#region using
using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories.CQRS;
using EscarGoLibrary.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
#endregion

namespace EscarGoLibrary.ViewModel
{
    public class TicketModelBuilderCQRS
    {
        #region Constructeur
        protected readonly IUnitOfWorkCQRS UnitOfWork;


        public TicketModelBuilderCQRS(IUnitOfWorkCQRS unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        #endregion

        #region GetTicketAsync
        public async Task<BuyTicketViewModel> GetTicketAsync(int courseId)
        {
            BuyTicketViewModel vm = new BuyTicketViewModel();

            List<Visiteur> visiteurs = await UnitOfWork.TicketRepositoryAsync.GetVisiteursAsync();
            vm.Acheteurs = new SelectList(visiteurs, "Id", "Nom");

            var entities = UnitOfWork.RaceRepository.GetRaceDetail(courseId);
            vm.Course = entities.First().ToCourse();

            vm.NbPlaces = 1;

            return vm;
        }
        #endregion

        #region PostTicketAsync (virtual)
        public virtual async Task<ConfirmationAchatViewModel> PostTicketAsync(BuyTicketViewModel buyTicketViewModel)
        {
            ConfirmationAchatViewModel vm = new ConfirmationAchatViewModel();
            vm.DateAchat = DateTime.Now;
            Ticket ticket = null;

            try
            {
                ticket = await UnitOfWork.TicketRepositoryAsync.AddTicketAsync(buyTicketViewModel.Course.CourseId, buyTicketViewModel.AcheteurSelectionne, buyTicketViewModel.NbPlaces);
                await UnitOfWork.SaveAsync();

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
