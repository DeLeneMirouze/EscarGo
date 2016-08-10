using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.Async
{
    public class UnitOfWorkAsync : IDisposable, IUnitOfWorkAsync
    {
        #region Constructeur
        public UnitOfWorkAsync()
        {
           Context = new EscarGoContext();
        }
        #endregion

        #region CompetitorRepositoryAsync
        ICompetitorRepositoryAsync _competitorRepository;
        public ICompetitorRepositoryAsync CompetitorRepositoryAsync
        {
            get
            {
                if (_competitorRepository == null)
                {
                    _competitorRepository = new CompetitorRepositoryAsync(Context);
                }
                return _competitorRepository;
            }
            private set
            {

                _competitorRepository = value;
            }
        }
        #endregion

        #region RaceRepositoryAsync
        IRaceRepositoryAsync _courseRepository;
        public IRaceRepositoryAsync RaceRepositoryAsync
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new RaceRepositoryAsync(Context);
                }
                return _courseRepository;
            }
            private set
            {

                _courseRepository = value;
            }
        }
        #endregion

        #region TicketRepositoryAsync
        ITicketRepositoryAsync _ticketRepository;
        public ITicketRepositoryAsync TicketRepositoryAsync
        {
            get
            {
                if (_ticketRepository == null)
                {
                    _ticketRepository = new TicketRepositoryAsync(Context);
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
            await Context.SaveChangesAsync();
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
