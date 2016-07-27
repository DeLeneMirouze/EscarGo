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
        readonly IUnitOfWorkAsync _unitOfWorkAsync;


        public ViewModelBuilderAsync(IUnitOfWorkAsync unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        #endregion

        #region GetCompetitorsAsync
        public async Task<List<Concurrent>> GetCompetitorsAsync()
        {
            return await _unitOfWorkAsync.CompetitorRepositoryAsync.GetCompetitorsAsync();
        }
        #endregion

        #region GetDetailConcurrentViewModelAsync
        public async Task<DetailConcurrentViewModel> GetDetailConcurrentViewModelAsync(int idConcurrent)
        {
            DetailConcurrentViewModel vm = new DetailConcurrentViewModel();
            vm.Concurrent = await _unitOfWorkAsync.CompetitorRepositoryAsync.GetCompetitorByIdAsync(idConcurrent);

            var paris = await _unitOfWorkAsync.CompetitorRepositoryAsync.GetBetsByCompetitorAsync(idConcurrent);
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
            await _unitOfWorkAsync.CompetitorRepositoryAsync.SetBetAsync(idCourse, idConcurrent);
            await _unitOfWorkAsync.SaveAsync();
        }
        #endregion

        #region GetDetailCourseViewModelAsync
        public async Task<DetailCourseViewModel> GetDetailCourseViewModelAsync(int idCourse)
        {
            DetailCourseViewModel vm = new DetailCourseViewModel();

            vm.Course = await _unitOfWorkAsync.RaceRepositoryAsync.GetCourseByIdAsync(idCourse);
            vm.Concurrents = await _unitOfWorkAsync.RaceRepositoryAsync.GetConcurrentsByRaceAsync(idCourse);
            var paris = await _unitOfWorkAsync.CompetitorRepositoryAsync.GetBetsByRaceAsync(idCourse);
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
           _unitOfWorkAsync.RaceRepositoryAsync.Create(course);
            await _unitOfWorkAsync.SaveAsync();
        }
        #endregion
    }
}
