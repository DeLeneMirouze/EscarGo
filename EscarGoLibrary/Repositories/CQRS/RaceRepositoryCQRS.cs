#region using
using EscarGoLibrary.Models;
using EscarGoLibrary.Storage.Model;
using EscarGoLibrary.Storage.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.Repositories.CQRS
{
    public class RaceRepositoryCQRS: BaseDataRepository, IRaceRepositoryCQRS
    {
        #region Constructeur
        ITableStorageRepository _storageRepository;
        public RaceRepositoryCQRS(EscarGoContext context) : base(context)
        {
            _storageRepository = new TableStorageRepository();
        }
        #endregion

        #region GetRaces
        public List<Course> GetRaces()
        {
            List<Course> courses = _storageRepository.GetRaces();
            courses = courses
                .Distinct(new RaceComparer())
                .OrderByDescending(c => c.Date)
                .ThenBy(c => c.Pays)
                .ThenBy(c => c.Label)
                .ToList();

            return courses;
        }
        #endregion

        #region GetRaceDetail
        public List<RaceEntity> GetRaceDetail(int idRace)
        {
            List<RaceEntity>  entities = _storageRepository.GetRaceById(idRace);

            return entities;
        }
        #endregion

        #region Create
        public void Create(Course course)
        {
            Context.Courses.Add(course);
        }
        #endregion

        #region Like

        public async Task LikeAsync(int idCourse)
        {
            Course course = await Context.Courses.FirstOrDefaultAsync(c => c.CourseId == idCourse);
            course.Likes++;
        }
        #endregion
    }
}
