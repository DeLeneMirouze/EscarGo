using EscarGoLibrary.ViewModel;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EscarGoCache.Controllers
{
    public class TicketsController : CustomControllerCache
    {
        // GET: Ticket
        public ActionResult Buy(int courseId)
        {
            BuyTicketViewModel vm = TicketModelBuilder.GetTicket(courseId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Buy(BuyTicketViewModel vm)
        {
            ConfirmationAchatViewModel confirmationAchatViewModel = await TicketModelBuilder.PostTicketAsync(vm);

            return View("ConfirmationAchatViewModel", confirmationAchatViewModel);
        }
    }
}