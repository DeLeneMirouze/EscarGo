using EscarGoLibrary.Repositories.Async;
using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.CQRS
{
    public class UnitOfWorkCQRS: BaseDataRepository, IUnitOfWorkCQRS, IDisposable
    {
        #region Constructeur
        public UnitOfWorkCQRS(EscarGoContext context):base(context)
        {

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
            await SqlAzureRetry.ExecuteAsync(async () => await Context.SaveChangesAsync());
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
    }
}
