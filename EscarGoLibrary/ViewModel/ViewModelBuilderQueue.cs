#region using
using EscarGoLibrary.Caching;
using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories.CQRS;
using EscarGoLibrary.Storage.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.ViewModel
{
    public class ViewModelBuilderQueue
    {
        #region Constructeur
        readonly IUnitOfWorkCQRS _unitOfWork;
        readonly IRedisRepository _redisRepository;


        public ViewModelBuilderQueue(IUnitOfWorkCQRS unitOfWork, IRedisRepository redisRepository)
        {
            _unitOfWork = unitOfWork;
            _redisRepository = redisRepository;
        }
        #endregion

        #region GetCompetitors
        public List<Concurrent> GetCompetitors()
        {
            return _unitOfWork.CompetitorRepository.GetCompetitors();
        }
        #endregion

        #region GetDetailConcurrentViewModelAsync
        public DetailConcurrentViewModel GetDetailConcurrentViewModelAsync(int idConcurrent)
        {
            DetailConcurrentViewModel vm = new DetailConcurrentViewModel();

            var entities = _redisRepository.GetCompetitorDetail(idConcurrent);
            vm.Concurrent = entities.First().ToConcurrent();
            foreach (var entity in entities)
            {
                Course course = entity.ToCourse();
                vm.Courses.Add(course);
            }
            vm.Courses = vm.Courses.OrderBy(c => c.Date).ToList();

            return vm;
        }
        #endregion

        #region SetBetAsync
        public async Task SetBetAsync(int idCourse, int idConcurrent)
        {
            await _unitOfWork.CompetitorRepository.SetBetAsync(idCourse, idConcurrent);
            await _unitOfWork.SaveAsync();
        }
        #endregion

        #region GetDetailCourseViewModel
        public DetailCourseViewModel GetDetailCourseViewModel(int idCourse)
        {
            DetailCourseViewModel vm = new DetailCourseViewModel();

            List<RaceEntity> entities = _redisRepository.GetRaceDetail(idCourse);
            vm.Course = entities.First().ToCourse();
            foreach (RaceEntity entity in entities)
            {
                Concurrent concurrent = entity.ToConcurrent();
                vm.Concurrents.Add(concurrent);
            }

            return vm;
        }
        #endregion

        #region CreateAsync
        public async Task CreateAsync(Course course)
        {
            _unitOfWork.RaceRepository.Create(course);
            await _unitOfWork.SaveAsync();
        }
        #endregion
    }
}
