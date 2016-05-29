using EscarGo.Repositories;
using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace EscarGo.Models
{
    public class ViewModelBuilder
    {
        #region Constructeur
        readonly ICompetitorRepository _concurrentRepository;
        readonly ICourseRepository _courseRepository;

        public ViewModelBuilder(ICompetitorRepository repository, ICourseRepository courseRepository)
        {
            _concurrentRepository = repository;
            _courseRepository = courseRepository;
        }
        #endregion

        #region GetConcurrents
        public List<Concurrent> GetConcurrents()
        {
            return _concurrentRepository.GetCompetitors();
        } 
        #endregion

        #region GetDetailConcurrentViewModel
        public DetailConcurrentViewModel GetDetailConcurrentViewModel(int idConcurrent)
        {
            DetailConcurrentViewModel vm = new DetailConcurrentViewModel();
            vm.Concurrent = _concurrentRepository.GetCompetitorById(idConcurrent);

            var paris = _concurrentRepository.GetBetsByCompetitor(idConcurrent);
            vm.Courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();
            foreach (var course in vm.Courses)
            {
                Pari currentBet = paris.Where(p => p.IdCourse == course.IdCourse).First();
                course.SC = currentBet.SC;
            }

            return vm;
        }
        #endregion

        #region SetBet
        public void SetBet(int idCourse, int idConcurrent)
        {
            _concurrentRepository.SetBet(idCourse, idConcurrent);
        }
        #endregion

        #region GetDetailCourseViewModel
        public DetailCourseViewModel GetDetailCourseViewModel(int idCourse)
        {
            DetailCourseViewModel vm = new DetailCourseViewModel();

            vm.Course = _courseRepository.GetCourseById(idCourse);
            vm.Concurrents = _courseRepository.GetConcurrentsByCourse(idCourse);
            var paris = _concurrentRepository.GetBetsByRace(idCourse);
            foreach (Concurrent concurrent in vm.Concurrents)
            {
                Pari currentBet = paris.Where(p => p.IdConcurrent == concurrent.IdConcurrent).First();
                concurrent.SC = currentBet.SC;
            }

            return vm;
        }
        #endregion
    }
}
