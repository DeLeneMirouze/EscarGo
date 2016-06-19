
using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EscarGoLibrary.ViewModel
{
    public class BuyTicketViewModel
    {
        public IEnumerable<SelectListItem> Acheteurs { get; set; }
        public Course Course { get; set; }
        public int AcheteurSelectionne { get; set; }
        public int NbPlaces { get; set; }
    }
}
