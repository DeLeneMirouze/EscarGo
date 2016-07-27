using EscarGoLibrary.Repositories;
using EscarGoLibrary.ViewModel;
using System.Web.Mvc;

namespace EscarGoLibrary.Models
{
    public abstract class CustomController : Controller
    {
        #region Constructeur
        protected CustomController()
        {
            UnitOfWork = new UnitOfWork();
            Builder = new ViewModelBuilder(UnitOfWork);
            TicketModelBuilder = new TicketModelBuilder(UnitOfWork);
        }
        #endregion

        protected IUnitOfWork UnitOfWork { get; set; }
        protected ViewModelBuilder Builder { get; set; }
        protected TicketModelBuilder TicketModelBuilder {get;set;}

        protected const int RecordsPerPage = 6;

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
