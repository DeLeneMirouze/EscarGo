using EscarGo.Repositories;
using System.Web.Mvc;

namespace EscarGo.Models
{
    public class CustomController: Controller
    {
        protected CustomController()
        {
            var context = new EscarGoContext();
            ConcurrentRepository = new ConcurrentRepository(context);
            CourseRepository = new CourseRepository(context);
            Builder = new ViewModelBuilder(ConcurrentRepository, CourseRepository);
        }

        protected IConcurrentRepository ConcurrentRepository { get; set; }
        protected ViewModelBuilder Builder { get; set; }
        protected ICourseRepository CourseRepository { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ConcurrentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
