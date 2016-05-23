using EscarGo.Repositories;
using System.Web.Mvc;

namespace EscarGo.Models
{
    public class CustomController: Controller
    {
        protected CustomController()
        {
            Repository = new DataRepository(new EscarGoContext());
            Builder = new ViewModelBuilder(Repository);
        }

        protected IDataRepository Repository { get; set; }
        protected ViewModelBuilder Builder { get; set; }

    }
}
