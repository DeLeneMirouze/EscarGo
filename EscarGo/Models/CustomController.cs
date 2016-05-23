using EscarGo.Repositories;
using System.Web.Mvc;

namespace EscarGo.Models
{
    public class CustomController: Controller
    {
        protected CustomController()
        {
            var context = new EscarGoContext();
            ConcurrentReposiory = new ConcurrentRepository(context);
            Builder = new ViewModelBuilder(ConcurrentReposiory);
            CourseRepository = new CourseRepository(context);
        }

        protected IConcurrentRepository ConcurrentReposiory { get; set; }
        protected ViewModelBuilder Builder { get; set; }
        protected ICourseRepository CourseRepository { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ConcurrentReposiory.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
