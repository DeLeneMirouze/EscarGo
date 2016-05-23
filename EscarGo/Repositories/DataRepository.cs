using EscarGo.Models;
using System.Collections.Generic;
using System.Linq;

namespace EscarGo.Repositories
{
    class DataRepository: BaseDataRepository, IDataRepository
    {
        public DataRepository(EscarGoContext context):base(context)
        {

        }

        #region GetConcurrents
        public List<Concurrent> GetConcurrents()
        {
            var concurrents = Context.Concurrents
          .Include("Pari")
          .OrderBy(c => c.Nom)
          .ToList();

            return concurrents;
        }
        #endregion

        #region GetConcurrentById
        public Concurrent GetConcurrentById(int id)
        {
            var concurrent = Context.Concurrents
          .Include("Pari").FirstOrDefault(c => c.IdConcurrent == id);

            return concurrent;
        }
        #endregion
    }
}
