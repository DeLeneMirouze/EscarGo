using EscarGoLibrary.Repositories.CQRS;
using EscarGoLibrary.ViewModel;
using System.Web.Mvc;

namespace EscarGoQueue.Controllers
{
    public abstract class CustomControllerQueue : Controller
    {
        public CustomControllerQueue()
        {
            UnitOfWork = new UnitOfWorkCQRS();
            Builder = new ViewModelBuilderCQRS(UnitOfWork);
            TicketModelBuilder = new TicketModelBuilderCQRS(UnitOfWork);
        }

        protected IUnitOfWorkCQRS UnitOfWork { get; set; }
        protected ViewModelBuilderCQRS Builder { get; set; }
        protected TicketModelBuilderCQRS TicketModelBuilder { get; set; }

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