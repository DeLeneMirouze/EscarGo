using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EscarGoLibrary.ViewModel
{
    public class ViewModelBuilder
    {
        #region Constructeur
        readonly IUnitOfWork _unitOfWork;

        public ViewModelBuilder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GetCompetitors
        public List<Concurrent> GetCompetitors()
        {
            return _unitOfWork.CompetitorRepository.GetCompetitors();
        }
        #endregion

        #region GetDetailConcurrentViewModel
        public DetailConcurrentViewModel GetDetailConcurrentViewModel(int idConcurrent)
        {
            DetailConcurrentViewModel vm = new DetailConcurrentViewModel();
            vm.Concurrent = _unitOfWork.CompetitorRepository.GetCompetitorById(idConcurrent);

            var paris = _unitOfWork.CompetitorRepository.GetBetsByCompetitor(idConcurrent);
            vm.Courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();
            foreach (var course in vm.Courses)
            {
                Pari currentBet = paris.Where(p => p.CourseId == course.CourseId).First();
                course.SC = currentBet.SC;
            }

            return vm;
        }
        #endregion

        #region SetBet
        public void SetBet(int idCourse, int idConcurrent)
        {
            _unitOfWork.CompetitorRepository.SetBet(idCourse, idConcurrent);
        }
        #endregion

        #region GetDetailCourseViewModel
        public DetailCourseViewModel GetDetailCourseViewModel(int idCourse)
        {
            DetailCourseViewModel vm = new DetailCourseViewModel();

            vm.Course = _unitOfWork.CourseRepository.GetRaceById(idCourse);
            vm.Concurrents = _unitOfWork.CourseRepository.GetConcurrentsByRace(idCourse);
            var paris = _unitOfWork.CompetitorRepository.GetBetsByRace(idCourse);
            foreach (Concurrent concurrent in vm.Concurrents)
            {
                Pari currentBet = paris.Where(p => p.ConcurrentId == concurrent.ConcurrentId).First();
                concurrent.SC = currentBet.SC;
            }

            return vm;
        }
        #endregion

        #region Create
        public void Create(Course course)
        {
            _unitOfWork.CourseRepository.Create(course);
        }
        #endregion
    }
}
