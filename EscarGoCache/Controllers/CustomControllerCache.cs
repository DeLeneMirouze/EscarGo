#region using
using EscarGoLibrary.Caching;
using EscarGoLibrary.Repositories;
using EscarGoLibrary.Repositories.CQRS;
using EscarGoLibrary.Storage.Repository;
using EscarGoLibrary.ViewModel;
using System.Web.Mvc;
#endregion

namespace EscarGoCache.Controllers
{
    public abstract class CustomControllerCache : Controller
    {
        #region Constructeur
        public CustomControllerCache()
        {
            UnitOfWork = new UnitOfWorkCQRS(new EscarGoContext());
            QueueRepositoryAsync = new QueueRepositoryAsync();
            ITableStorageRepository tablestorageRepository = new TableStorageRepository();
            IRedisRepository redisRepository = new RedisRepository(tablestorageRepository);
            Builder = new ViewModelBuilderQueue(UnitOfWork, redisRepository);
            TicketModelBuilder = new TicketModelBuilderCache(UnitOfWork, QueueRepositoryAsync);
        } 
        #endregion

        protected IUnitOfWorkCQRS UnitOfWork { get; set; }
        protected ViewModelBuilderQueue Builder { get; set; }
        protected TicketModelBuilderCache TicketModelBuilder { get; set; }
        protected IQueueRepositoryAsync QueueRepositoryAsync { get; set; }

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}