using System;

namespace EscarGo.Repositories
{
    abstract class BaseDataRepository: IDisposable
    {
        protected BaseDataRepository(EscarGoContext context)
        {
            Context = context;
        }

        protected EscarGoContext Context { get; set; }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        } 
        #endregion
    }
}
