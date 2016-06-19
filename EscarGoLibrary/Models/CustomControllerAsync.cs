using EscarGoLibrary.Repositories;
using EscarGoLibrary.ViewModel;
using System.Web.Mvc;

namespace EscarGoLibrary.Models
{
    public abstract class CustomControllerAsync: Controller
    {
        #region Constructeur
        protected CustomControllerAsync()
        {
            var context = new EscarGoContext();
            ConcurrentRepository = new CompetitorRepository(context);
            CourseRepository = new CourseRepository(context);
            Builder = new ViewModelBuilderAsync(ConcurrentRepository, CourseRepository);
            TicketRepository = new TicketRepository(context);
            TicketModelBuilder = new TicketModelBuilderAsync(TicketRepository, CourseRepository);
        }
        #endregion

        protected ICompetitorRepositoryAsync ConcurrentRepository { get; set; }
        protected ViewModelBuilderAsync Builder { get; set; }
        protected ICourseRepositoryAsync CourseRepository { get; set; }
        protected TicketModelBuilderAsync TicketModelBuilder { get; set; }
        protected ITicketRepositoryAsync TicketRepository { get; set; }

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
