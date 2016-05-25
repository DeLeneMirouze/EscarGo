using System.Net;
using System.Web.Mvc;
using EscarGo.Models;
using EscarGo.Repositories;

namespace EscarGo.Controllers
{
    public class CoursesController : CustomController
    {
        private EscarGoContext db = new EscarGoContext();

        // GET: Courses
        public ActionResult Index()
        {
            return View(CourseRepository.GetCourses());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vm = Builder.GetDetailCourseViewModel(id.Value);
            if (vm.Course == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        public ActionResult Bet(int idCourse, int idConcurrent)
        {
            if (idConcurrent == 0 || idConcurrent == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Builder.SetBet(idCourse, idConcurrent);
            return Redirect("Details/" + idCourse.ToString());
        }
    }
}
