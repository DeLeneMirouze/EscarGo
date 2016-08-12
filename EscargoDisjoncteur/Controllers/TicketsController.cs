using EscarGoLibrary.ViewModel;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EscarGoDisjoncteur.Controllers
{
    public class TicketsController: CustomControllerAsync
    {
        // GET: Ticket
        public async Task<ActionResult> Buy(int courseId)
        {
            BuyTicketViewModel vm = await TicketModelBuilder.GetTicketAsync(courseId);
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
