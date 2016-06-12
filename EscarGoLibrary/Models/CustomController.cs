using EscarGo.Repositories;
using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories;
using System.Web.Mvc;

namespace EscarGo.Models
{
    public class CustomController : Controller
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
