using System.Net;
using System.Web.Mvc;
using EscarGo.Models;
using EscarGoLibrary.Models;

namespace EscarGo.Controllers
{
    public class ConcurrentsController : CustomController
    {
        // GET: Concurrents
        public ActionResult Index()
        {
            var concurrents = Builder.GetConcurrents();

            return View(concurrents);
        }

        // GET: Concurrents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailConcurrentViewModel vm = Builder.GetDetailConcurrentViewModel(id.Value);
            if (vm.Concurrent == null)
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
            return Redirect("Details/" + idConcurrent.ToString());
        }
    }
}
