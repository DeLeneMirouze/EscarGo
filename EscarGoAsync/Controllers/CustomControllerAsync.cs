using EscarGoLibrary.Repositories;
using EscarGoLibrary.Repositories.Async;
using EscarGoLibrary.ViewModel;
using System.Web.Mvc;

namespace EscarGoAsync.Controllers
{
    public abstract class CustomControllerAsync : Controller
    {
        #region Constructeur
        protected CustomControllerAsync()
        {
            UnitOfWorkAsync = new UnitOfWorkAsync(new EscarGoContext());
            Builder = new ViewModelBuilderAsync(UnitOfWorkAsync);
            TicketModelBuilder = new TicketModelBuilderAsync(UnitOfWorkAsync);
        }
        #endregion

        protected IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
        protected ViewModelBuilderAsync Builder { get; set; }
        protected TicketModelBuilderAsync TicketModelBuilder { get; set; }

        protected const int RecordsPerPage = 6;

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}