using EscarGoLibrary.Models;
using System.Web.Mvc;

namespace EscarGo.Controllers
{
    public class TicketsController : CustomController
    {
        // GET: Ticket
        public ActionResult Buy(int courseId)
        {
            BuyTicketViewModel vm = TicketModelBuilder.GetTicket(courseId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(BuyTicketViewModel vm)
        {
            TicketModelBuilder.PostTicket(vm);
            return RedirectToAction("Index", "Courses");
        }
    }
}