using EscarGoLibrary.Models;
using System.Web.Mvc;

namespace EscarGo.Controllers
{
    public class TicketController : CustomController
    {
        // GET: Ticket
        public ActionResult Buy(int idCourse)
        {
            BuyTicketViewModel vm = TicketModelBuilder.Buy(idCourse);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(BuyTicketViewModel vm)
        {
            //var vm = TicketModelBuilder.Buy(idCourse);
            return View(vm);
        }
    }
}