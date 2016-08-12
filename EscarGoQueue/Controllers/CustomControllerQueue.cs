using EscarGoLibrary.Repositories;
using EscarGoLibrary.Repositories.CQRS;
using EscarGoLibrary.Storage.Repository;
using EscarGoLibrary.ViewModel;
using System.Web.Mvc;

namespace EscarGoCache.Controllers
{
    public abstract class CustomControllerQueue : Controller
    {
        #region Constructeur
        public CustomControllerQueue()
        {
            UnitOfWork = new UnitOfWorkCQRS(new EscarGoContext());
            QueueRepositoryAsync = new QueueRepositoryAsync();
            Builder = new ViewModelBuilderCQRS(UnitOfWork);
            TicketModelBuilder = new TicketModelBuilderQueue(UnitOfWork, QueueRepositoryAsync);
        } 
        #endregion

        protected IUnitOfWorkCQRS UnitOfWork { get; set; }
        protected ViewModelBuilderCQRS Builder { get; set; }
        protected TicketModelBuilderQueue TicketModelBuilder { get; set; }
        protected IQueueRepositoryAsync QueueRepositoryAsync { get; set; }

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}