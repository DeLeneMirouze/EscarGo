using System;

namespace EscarGoLibrary.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        ICompetitorRepository CompetitorRepository { get; }
        ICourseRepository CourseRepository { get; }
        ITicketRepository TicketRepository { get; }

        void Save();
    }
}