using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.Async
{
    public class UnitOfWorkAsync : BaseDataRepository, IDisposable, IUnitOfWorkAsync
    {
        #region Constructeur
        public UnitOfWorkAsync(EscarGoContext context):base(context)
        {
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
            await SqlAzureRetry.ExecuteAsync(async () =>
                    {
                        await Context.SaveChangesAsync();
                    }
                );
        }
        #endregion
    }
}
