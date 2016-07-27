using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public interface IUnitOfWorkAsync: IDisposable
    {
        ICompetitorRepositoryAsync CompetitorRepositoryAsync { get; }
        IRaceRepositoryAsync RaceRepositoryAsync { get; }
        ITicketRepositoryAsync TicketRepositoryAsync { get; }

        Task SaveAsync();
    }
}