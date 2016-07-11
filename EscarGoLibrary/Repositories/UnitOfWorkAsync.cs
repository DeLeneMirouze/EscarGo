using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public class UnitOfWorkAsync : IDisposable, IUnitOfWorkAsync
    {
        #region Constructeur
        private EscarGoContext _context;

        public UnitOfWorkAsync()
        {
            _context = new EscarGoContext();
        }
        #endregion

        #region ICompetitorRepositoryAsync
        ICompetitorRepositoryAsync _competitorRepository;
        public ICompetitorRepositoryAsync CompetitorRepositoryAsync
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

        #region ICourseRepositoryAsync
        ICourseRepositoryAsync _courseRepository;
        public ICourseRepositoryAsync CourseRepositoryAsync
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new CourseRepository(_context);
                }
                return _courseRepository;
            }
            private set
            {

                _courseRepository = value;
            }
        }
        #endregion

        #region ITicketRepositoryAsync
        ITicketRepositoryAsync _ticketRepository;
        public ITicketRepositoryAsync TicketRepositoryAsync
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

        #region SaveAsync
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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
