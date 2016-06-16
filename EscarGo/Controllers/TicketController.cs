using EscarGoLibrary.Models;
using EscarGoLibrary.Models;
using System.Web.Mvc;

namespace EscarGo.Controllers
{
    public class TicketController : CustomController
    {
        // GET: Ticket
        public ActionResult Buy(int idCourse)
        {
            var vm = TicketModelBuilder.Buy(idCourse);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Buy(BuyTicketViewModel vm)
        {
            //var vm = TicketModelBuilder.Buy(idCourse);
            return View(vm);
        }
    }
}