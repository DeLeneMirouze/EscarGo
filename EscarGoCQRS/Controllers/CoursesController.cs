using EscarGoLibrary.Models;
using System.Net;
using System.Web.Mvc;

namespace EscarGoCQRS.Controllers
{
    public class CoursesController : CustomController
    {
        // GET: Courses
        public ActionResult Index(int? currentPage)
        {
            return View(UnitOfWork.CourseRepository.GetCourses(RecordsPerPage, currentPage.GetValueOrDefault()));
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
