using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscarGoLibrary.ViewModel
{
    public sealed class ViewModelBuilderAsync
    {
        #region Constructeur
        readonly ICompetitorRepositoryAsync _concurrentRepository;
        readonly ICourseRepositoryAsync _courseRepository;

        public ViewModelBuilderAsync(ICompetitorRepositoryAsync repository, ICourseRepositoryAsync courseRepository)
        {
            _concurrentRepository = repository;
            _courseRepository = courseRepository;
        }
        #endregion

        #region GetCompetitorsAsync
        public async Task<List<Concurrent>> GetCompetitorsAsync()
        {
            return await _concurrentRepository.GetCompetitorsAsync();
        }
        #endregion

        #region GetDetailConcurrentViewModelAsync
        public async Task<DetailConcurrentViewModel> GetDetailConcurrentViewModelAsync(int idConcurrent)
        {
            DetailConcurrentViewModel vm = new DetailConcurrentViewModel();
            vm.Concurrent = await _concurrentRepository.GetCompetitorByIdAsync(idConcurrent);

            var paris = await _concurrentRepository.GetBetsByCompetitorAsync(idConcurrent);
            vm.Courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();
            foreach (var course in vm.Courses)
            {
                Pari currentBet = paris.Where(p => p.CourseId == course.CourseId).First();
                course.SC = currentBet.SC;
            }

            return vm;
        }
        #endregion

        #region SetBetAsync
        public async Task SetBetAsync(int idCourse, int idConcurrent)
        {
            await _concurrentRepository.SetBetAsync(idCourse, idConcurrent);
        }
        #endregion

        #region GetDetailCourseViewModelAsync
        public async Task<DetailCourseViewModel> GetDetailCourseViewModelAsync(int idCourse)
        {
            DetailCourseViewModel vm = new DetailCourseViewModel();

            vm.Course = await _courseRepository.GetCourseByIdAsync(idCourse);
            vm.Concurrents = await _courseRepository.GetConcurrentsByCourseAsync(idCourse);
            var paris = await _concurrentRepository.GetBetsByRaceAsync(idCourse);
            foreach (Concurrent concurrent in vm.Concurrents)
            {
                Pari currentBet = paris.Where(p => p.ConcurrentId == concurrent.ConcurrentId).First();
                concurrent.SC = currentBet.SC;
            }

            return vm;
        }
        #endregion

        #region CreateAsync
        public async Task CreateAsync(Course course)
        {
           await  _courseRepository.CreateAsync(course);
        }
        #endregion
    }
}
