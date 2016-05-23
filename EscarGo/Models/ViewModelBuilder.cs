using EscarGo.Repositories;
using System.Collections.Generic;

namespace EscarGo.Models
{
    public class ViewModelBuilder
    {
        #region Constructeur
        private IDataRepository _repository;

        public ViewModelBuilder(IDataRepository repository)
        {
            _repository = repository;
        } 
        #endregion

        public List<Concurrent> GetConcurrents()
        {
            return _repository.GetConcurrents();
        }

        public Concurrent GetConcurrentById(int id)
        {
            return _repository.GetConcurrentById(id);
        }
    }
}
