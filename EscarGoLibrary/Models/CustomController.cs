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
            var context = new EscarGoContext();
            ConcurrentRepository = new CompetitorRepository(context);
            CourseRepository = new CourseRepository(context);
            Builder = new ViewModelBuilder(ConcurrentRepository, CourseRepository);
            TicketRepository = new TicketRepository(context);
            TicketModelBuilder = new TicketModelBuilder(TicketRepository, CourseRepository);
        }
        #endregion

        protected ICompetitorRepository ConcurrentRepository { get; set; }
        protected ViewModelBuilder Builder { get; set; }
        protected ICourseRepository CourseRepository { get; set; }
        protected TicketModelBuilder TicketModelBuilder {get;set;}
        protected ITicketRepository TicketRepository { get; set; }

        protected const int RecordsPerPage = 6;

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ConcurrentRepository.Dispose();
            }
            base.Dispose(disposing);
        } 
        #endregion
    }
}
