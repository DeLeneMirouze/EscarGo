using System;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.CQRS
{
    public class UnitOfWorkCQRS: IUnitOfWorkCQRS, IDisposable
    {
        #region Constructeur
        private EscarGoContext _context;
        public UnitOfWorkCQRS()
        {
            _context = new EscarGoContext();
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
                    _courseRepository = new RaceRepositoryCQRS(_context);
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
