using EscarGoLibrary.Repositories;
using System.Collections.Generic;
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

            List<Visiteur> visiteurs = _ticketRepository.GetVisiteurs();
            vm.Acheteurs = new SelectList(visiteurs, "Id", "Nom");
            vm.Course = _courseRepository.GetCourseById(idCourse);
            vm.NbPlaces = 1;

            return vm;
        } 
        #endregion
    }
}
