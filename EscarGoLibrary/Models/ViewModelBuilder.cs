using EscarGo.Repositories;
using System.Collections.Generic;

namespace EscarGo.Models
{
    public class ViewModelBuilder
    {
        #region Constructeur
        private IConcurrentRepository _repository;

        public ViewModelBuilder(IConcurrentRepository repository)
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
