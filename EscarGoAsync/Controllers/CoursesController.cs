using System.Net;
using System.Web.Mvc;
using EscarGo.Models;
using EscarGo.Repositories;
using System.Threading.Tasks;

namespace EscarGoAsync.Controllers
{
    public class CoursesController : CustomController
    {
        private EscarGoContext db = new EscarGoContext();

        #region Index
        // GET: Courses
        public ActionResult Index()
        {
            return View(CourseRepository.GetCourses());
        } 
        #endregion

        #region Details
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
        #endregion

        #region Bet
        public ActionResult Bet(int idCourse, int idConcurrent)
        {
            if (idConcurrent == 0 || idConcurrent == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Builder.SetBet(idCourse, idConcurrent);
            return Redirect("Details/" + idCourse.ToString());
        }
        #endregion

        #region Create
        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                Builder.Create(course);
                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region Like
        // GET: Default/Like/5
        public async Task<ActionResult> Like(int id)
        {
            await CourseRepository.LikeAsync(id);
            return View("Index", await CourseRepository.GetCoursesAsync());
        }
        #endregion
    }
}
