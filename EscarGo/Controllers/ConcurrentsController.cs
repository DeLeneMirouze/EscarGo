using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EscarGo.Models;
using EscarGo.Repositories;

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
            if (id == null && id.Value == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concurrent concurrent = Builder.GetConcurrentById(id.Value);
            if (concurrent == null)
            {
                return HttpNotFound();
            }
            return View(concurrent);
        }
    }
}
