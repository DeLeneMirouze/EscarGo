using System;

namespace EscarGoLibrary.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        ICompetitorRepository CompetitorRepository { get; }
        IRaceRepository CourseRepository { get; }
        ITicketRepository TicketRepository { get; }

        void Save();
    }
}