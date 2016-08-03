using EscarGoLibrary.Repositories.CQRS;
using EscarGoLibrary.ViewModel;
using System.Web.Mvc;

namespace EscarGoCQRS.Controllers
{
    public abstract class CustomControllerCQRS : Controller
    {
        #region Constructeur
        protected CustomControllerCQRS()
        {
            UnitOfWork = new UnitOfWorkCQRS();
            Builder = new ViewModelBuilderCQRS(UnitOfWork);
            TicketModelBuilder = new TicketModelBuilderCQRS(UnitOfWork);
        }
        #endregion

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