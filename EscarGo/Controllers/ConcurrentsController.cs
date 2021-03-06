﻿using EscarGoLibrary.Models;
using EscarGoLibrary.ViewModel;
using System.Net;
using System.Web.Mvc;

namespace EscarGo.Controllers
{
    public class ConcurrentsController : CustomController
    {
        // GET: Concurrents
        public ActionResult Index()
        {
            var concurrents = Builder.GetCompetitors();

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

        public ActionResult Bet(int courseId, int concurrentId)
        {
            if (concurrentId == 0 || concurrentId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Builder.SetBet(courseId, concurrentId);
            return Redirect("Details/" + concurrentId.ToString());
        }
    }
}
