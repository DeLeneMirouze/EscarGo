#region using
using EscarGoLibrary.Caching;
using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories;
using EscarGoLibrary.Repositories.CQRS;
using EscarGoLibrary.Storage.Model;
using EscarGoLibrary.Storage.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
#endregion

namespace EscarGoLibrary.ViewModel
{
    public class TicketModelBuilderCache: TicketModelBuilderCQRS
    {
        #region Constructeur
        protected readonly IUnitOfWorkCQRS UnitOfWork;
        private readonly ITicketRepository _ticketRepository;
        readonly IQueueRepositoryAsync _queueRepositoryAsync;

        public TicketModelBuilderCache(IUnitOfWorkCQRS unitOfWork, IQueueRepositoryAsync queueRepositoryAsync):base(unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _ticketRepository = new TicketRepository(unitOfWork.Context);
            _queueRepositoryAsync = queueRepositoryAsync;
        }
        #endregion

        #region GetTicket
        public BuyTicketViewModel GetTicket(int courseId)
        {
            BuyTicketViewModel vm = new BuyTicketViewModel();

            List<Visiteur> visiteurs = LocalCache.Get<List<Visiteur>>("visiteurs", _ticketRepository.GetVisiteurs);
            vm.Acheteurs = new SelectList(visiteurs, "Id", "Nom");

            var entities = UnitOfWork.RaceRepository.GetRaceDetail(courseId);
            vm.Course = entities.First().ToCourse();

            vm.NbPlaces = 1;

            return vm;
        }
        #endregion

        #region PostTicketAsync
        public override async Task<ConfirmationAchatViewModel> PostTicketAsync(BuyTicketViewModel buyTicketViewModel)
        {
            ConfirmationAchatViewModel vm = new ConfirmationAchatViewModel();
            vm.DateAchat = DateTime.Now;

            try
            {
                string message = string.Format("{0},{1},{2}", buyTicketViewModel.Course.CourseId, buyTicketViewModel.AcheteurSelectionne, buyTicketViewModel.NbPlaces);
                await _queueRepositoryAsync.AddMessageAsync(message);

                vm.EstEnregistre = true;
                vm.Course = buyTicketViewModel.Course;
            }
            catch (Exception)
            {
                vm.EstEnregistre = false;
            }

            if (vm.EstEnregistre)
            {
                vm.Message = "Nous avons pré-enregistré votre achat. Vous devez attendre sa confirmation par email pour qu'il soit définitif";
                vm.NbTickets = buyTicketViewModel.NbPlaces;
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
