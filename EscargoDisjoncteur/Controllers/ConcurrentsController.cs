﻿using EscarGoLibrary.ViewModel;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EscarGoDisjoncteur.Controllers
{
    public class ConcurrentsController : CustomControllerAsync
    {
        // GET: Concurrents
        public async Task<ActionResult> Index()
        {
            var concurrents = await Builder.GetCompetitorsAsync();

            return View(concurrents);
        }

        // GET: Concurrents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailConcurrentViewModel vm =await  Builder.GetDetailConcurrentViewModelAsync(id.Value);
            if (vm.Concurrent == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        public async Task<ActionResult> Bet(int courseId, int concurrentId)
        {
            if (concurrentId == 0 || concurrentId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await Builder.SetBetAsync(courseId, concurrentId);
            return Redirect("Details/" + concurrentId.ToString());
        }
    }
}
