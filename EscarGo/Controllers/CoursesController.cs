using System.Net;
using System.Web.Mvc;
using EscarGo.Models;
using EscarGo.Repositories;

namespace EscarGo.Controllers
{
    public class CoursesController : CustomController
    {
        //private EscarGoContext db = new EscarGoContext();

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

        #region Get
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

        #region Edit
        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
