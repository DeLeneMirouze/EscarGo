using System;

namespace EscarGoLibrary.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        ICompetitorRepository CompetitorRepository { get; }
        IRaceRepository RaceRepository { get; }
        ITicketRepository TicketRepository { get; }

        void Save();
    }
}