#region using
using EscarGoLibrary.Repositories.CQRS;
using EscarGoLibrary.Storage.Repository;
using System;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.ViewModel
{
    public sealed class TicketModelBuilderQueue: TicketModelBuilderCQRS
    {
        #region Constructeur
        readonly IQueueRepositoryAsync _queueRepositoryAsync;


        public TicketModelBuilderQueue(IUnitOfWorkCQRS unitOfWork, IQueueRepositoryAsync queueRepositoryAsync) :base(unitOfWork)
        {
            _queueRepositoryAsync = queueRepositoryAsync;
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
