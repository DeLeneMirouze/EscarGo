using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EscarGo.Models;
using EscarGo.Repositories;

namespace EscarGo.Controllers
{
    public class ConcurrentsController : Controller
    {
        private EscarGoContext db = new EscarGoContext();

        // GET: Concurrents
        public ActionResult Index()
        {
            return View(db.Concurrents
                .OrderBy(c => c.Nom)
                .ToList());
        }

        // GET: Concurrents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concurrent concurrent = db.Concurrents.Find(id);
            if (concurrent == null)
            {
                return HttpNotFound();
            }
            return View(concurrent);
        }

        // GET: Concurrents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Concurrents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdConcurrent,Nom,Victoires,Defaites,Entraineur")] Concurrent concurrent)
        {
            if (ModelState.IsValid)
            {
                db.Concurrents.Add(concurrent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(concurrent);
        }

        // GET: Concurrents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concurrent concurrent = db.Concurrents.Find(id);
            if (concurrent == null)
            {
                return HttpNotFound();
            }
            return View(concurrent);
        }

        // POST: Concurrents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdConcurrent,Nom,Victoires,Defaites,Entraineur")] Concurrent concurrent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concurrent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(concurrent);
        }

        // GET: Concurrents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concurrent concurrent = db.Concurrents.Find(id);
            if (concurrent == null)
            {
                return HttpNotFound();
            }
            return View(concurrent);
        }

        // POST: Concurrents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Concurrent concurrent = db.Concurrents.Find(id);
            db.Concurrents.Remove(concurrent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
