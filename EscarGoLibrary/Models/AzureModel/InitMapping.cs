using AutoMapper;
using EscarGoLibrary.ViewModel.AzureModel;

namespace EscarGoLibrary.Models.AzureModel
{
    public static class InitMapping
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CompetitorEntity, Concurrent>()
                .ForMember(dest => dest.ConcurrentId, op => op.MapFrom(s => s.RowKey));

                cfg.CreateMap<RaceEntity, Course>()
                .ForMember(dest => dest.CourseId, op => op.MapFrom(s => s.RowKey));
            });

        }
    }
}
