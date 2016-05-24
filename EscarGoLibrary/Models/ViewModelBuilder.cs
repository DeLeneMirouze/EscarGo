using EscarGo.Repositories;
using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Linq;

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

        public DetailConcurrentViewModel GetDetailConcurrentViewModel(int id)
        {
            DetailConcurrentViewModel vm = new DetailConcurrentViewModel();
            vm.Concurrent = _repository.GetConcurrentById(id);

            var paris = _repository.GetParisByConcurrent(vm.Concurrent.IdConcurrent);
            vm.Courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();
            foreach(var course in vm.Courses)
            {
                Pari current = paris.Where(p => p.IdCourse == course.IdCourse).First();
                course.SC = current.SC;
            }

            return vm;
        }

        public void SetBet(int idCourse, int idConcurrent)
        {
            _repository.SetBet(idCourse, idConcurrent);
        }
    }
}
