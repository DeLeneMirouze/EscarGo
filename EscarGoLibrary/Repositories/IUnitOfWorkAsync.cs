using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public interface IUnitOfWorkAsync: IDisposable
    {
        ICompetitorRepositoryAsync CompetitorRepositoryAsync { get; }
        IRaceRepositoryAsync CourseRepositoryAsync { get; }
        ITicketRepositoryAsync TicketRepositoryAsync { get; }

        Task SaveAsync();
    }
}