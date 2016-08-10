using EscarGoLibrary.Repositories.Async;
using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.CQRS
{
    public class UnitOfWorkCQRS: IUnitOfWorkCQRS, IDisposable
    {
        #region Constructeur
        public UnitOfWorkCQRS()
        {
            Context = new EscarGoContext();
        }
        #endregion

        #region CompetitorRepository
        ICompetitorRepositoryCQRS _competitorRepository;
        public ICompetitorRepositoryCQRS CompetitorRepository
        {
            get
            {
                if (_competitorRepository == null)
                {
                    _competitorRepository = new CompetitorRepositoryCQRS(Context);
                }
                return _competitorRepository;
            }
            private set
            {

                _competitorRepository = value;
            }
        }
        #endregion

        #region RaceRepository
        IRaceRepositoryCQRS _courseRepository;
        public IRaceRepositoryCQRS RaceRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new RaceRepositoryCQRS(Context);
                }
                return _courseRepository;
            }
            private set
            {

                _courseRepository = value;
            }
        }
        #endregion

        #region SaveAsync
        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
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
