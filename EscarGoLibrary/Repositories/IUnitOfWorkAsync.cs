using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public interface IUnitOfWorkAsync: IDisposable
    {
        ICompetitorRepositoryAsync CompetitorRepositoryAsync { get; }
        ICourseRepositoryAsync CourseRepositoryAsync { get; }
        ITicketRepositoryAsync TicketRepositoryAsync { get; }

        Task SaveAsync();
    }
}