using System;

namespace EscarGoLibrary.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Constructeur
        public UnitOfWork()
        {
            Context = new EscarGoContext();
        }
        #endregion

        #region CompetitorRepository
        ICompetitorRepository _competitorRepository;
        public ICompetitorRepository CompetitorRepository
        {
            get
            {
                if (_competitorRepository == null)
                {
                    _competitorRepository = new CompetitorRepository(Context);
                }
                return _competitorRepository;
            }
            private set
            {

                _competitorRepository = value;
            }
        }
        #endregion

        #region CourseRepository
        IRaceRepository _courseRepository;
        public IRaceRepository RaceRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new RaceRepository(Context);
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
                    _ticketRepository = new TicketRepository(Context);
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
            Context.SaveChanges();
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
                    Context.Dispose();
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

        public EscarGoContext Context { get; private set; }
    }
}
