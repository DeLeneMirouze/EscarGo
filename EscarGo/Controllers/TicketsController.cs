using EscarGoLibrary.ViewModel;
using System.Web.Mvc;

namespace EscarGo.Controllers
{
    public class TicketsController : CustomController
    {
        // GET: Ticket by Id
        public ActionResult Buy(int courseId)
        {
            BuyTicketViewModel vm = TicketModelBuilder.GetTicket(courseId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(BuyTicketViewModel vm)
        {
            ConfirmationAchatViewModel confirmationAchatViewModel = TicketModelBuilder.PostTicket(vm);

            return View("ConfirmationAchatViewModel", confirmationAchatViewModel);
        }
    }
}
