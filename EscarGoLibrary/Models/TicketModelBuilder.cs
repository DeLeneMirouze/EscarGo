using EscarGoLibrary.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace EscarGoLibrary.Models
{
    public class TicketModelBuilder
    {
        #region Constructeur
        readonly ITicketRepository _ticketRepository;
        readonly ICourseRepository _courseRepository;

        public TicketModelBuilder(ITicketRepository ticketRepository, ICourseRepository courseRepository)
        {
            _ticketRepository = ticketRepository;
            _courseRepository = courseRepository;
        }
        #endregion

        #region Buy
        public BuyTicketViewModel Buy(int idCourse)
        {
            BuyTicketViewModel vm = new BuyTicketViewModel();

            var visiteurs = _ticketRepository.GetVisiteurs();
            vm.Acheteurs = visiteurs.Select(v => new SelectListItem() { Text = v.Nom, Value = v.Id.ToString() });
            vm.Course = _courseRepository.GetCourseById(idCourse);
            vm.NbPlaces = 1;

            return vm;
        } 
        #endregion
    }
}
