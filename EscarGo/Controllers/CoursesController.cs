using System.Data.Entity;
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
            if (id == null && id.Value == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = CourseRepository.GetCourseById(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        

    }
}
