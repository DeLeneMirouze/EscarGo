using System;

namespace EscarGoLibrary.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Constructeur
        private EscarGoContext _context;

        public UnitOfWork()
        {
            _context = new EscarGoContext();
        }
        #endregion

        #region ICompetitorRepository
        ICompetitorRepository _competitorRepository;
        public ICompetitorRepository CompetitorRepository
        {
            get
            {
                if (_competitorRepository == null)
                {
                    _competitorRepository = new CompetitorRepository(_context);
                }
                return _competitorRepository;
            }
            private set
            {

                _competitorRepository = value;
            }
        }
        #endregion

        #region ICourseRepository
        IRaceRepository _courseRepository;
        public IRaceRepository CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new RaceRepository(_context);
                }
                return _courseRepository;
            }
            private set
            {

                _courseRepository = value;
            }
        }
        #endregion

        #region ITicketRepository
        ITicketRepository _ticketRepository;
        public ITicketRepository TicketRepository
        {
            get
            {
                if (_ticketRepository == null)
                {
                    _ticketRepository = new TicketRepository(_context);
                }
                return _ticketRepository;
            }
            private set
            {

                _ticketRepository = value;
            }
        }
        #endregion

        #region Save
        public void Save()
        {
            _context.SaveChanges();
        } 
        #endregion

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
