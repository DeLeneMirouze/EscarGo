using AutoMapper;
using EscarGoLibrary.ViewModel.AzureModel;

namespace EscarGoLibrary.Models.AzureModel
{
    public static class ModelExtension
    {
        public static Concurrent ToConcurrent(this CompetitorEntity entity)
        {
            Concurrent concurrent = Mapper.Map<Concurrent>(entity);

            return concurrent;
        }

        public static Course ToCourse(this RaceEntity entity)
        {
            Course course = Mapper.Map<Course>(entity);

            return course;
        }
    }
}
