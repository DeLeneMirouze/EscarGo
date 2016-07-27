using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories.CQRS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscarGoLibrary.ViewModel
{
    public class ViewModelBuilderCQRS
    {
        #region Constructeur
        readonly IUnitOfWorkCQRS _unitOfWorkAsync;


        public ViewModelBuilderCQRS(IUnitOfWorkCQRS unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        #endregion

        #region GetCompetitors
        public List<Concurrent> GetCompetitors()
        {
            return _unitOfWorkAsync.CompetitorRepository.GetCompetitors();
        }
        #endregion

        #region GetDetailConcurrentViewModelAsync
        public async Task<DetailConcurrentViewModel> GetDetailConcurrentViewModelAsync(int idConcurrent)
        {
            DetailConcurrentViewModel vm = new DetailConcurrentViewModel();
            //vm.Concurrent = await _unitOfWorkAsync.CompetitorRepository.GetCompetitorByIdAsync(idConcurrent);

            //var paris = await _unitOfWorkAsync.CompetitorRepository.GetBetsByCompetitorAsync(idConcurrent);
            //vm.Courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();
            //foreach (var course in vm.Courses)
            //{
            //    Pari currentBet = paris.Where(p => p.CourseId == course.CourseId).First();
            //    course.SC = currentBet.SC;
            //}

            return vm;
        }
        #endregion

        //#region SetBetAsync
        //public async Task SetBetAsync(int idCourse, int idConcurrent)
        //{
        //    await _unitOfWorkAsync.CompetitorRepository.SetBetAsync(idCourse, idConcurrent);
        //    await _unitOfWorkAsync.SaveAsync();
        //}
        //#endregion

        #region GetDetailCourseViewModelAsync
        public async Task<DetailCourseViewModel> GetDetailCourseViewModelAsync(int idCourse)
        {
            DetailCourseViewModel vm = new DetailCourseViewModel();

            //vm.Course = await _unitOfWorkAsync.RaceRepository.GetCourseByIdAsync(idCourse);
            //vm.Concurrents = await _unitOfWorkAsync.RaceRepository.GetConcurrentsByRaceAsync(idCourse);
            //var paris = await _unitOfWorkAsync.CompetitorRepository.GetBetsByRaceAsync(idCourse);
            //foreach (Concurrent concurrent in vm.Concurrents)
            //{
            //    Pari currentBet = paris.Where(p => p.ConcurrentId == concurrent.ConcurrentId).First();
            //    concurrent.SC = currentBet.SC;
            //}

            return vm;
        }
        #endregion

        #region CreateAsync
        public async Task CreateAsync(Course course)
        {
            _unitOfWorkAsync.RaceRepository.Create(course);
            await _unitOfWorkAsync.SaveAsync();
        }
        #endregion
    }
}
