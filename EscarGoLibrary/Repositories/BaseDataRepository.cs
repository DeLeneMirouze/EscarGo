using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;

namespace EscarGoLibrary.Repositories
{
    public abstract class BaseDataRepository: IDisposable
    {
        #region Constructeur
        protected BaseDataRepository(EscarGoContext context)
        {
            Context = context;

            // On recommence jusqu'à 5 fois, au bout d'une seconde pour la première fois 
            // on incrémente de 2 secondes supplémentaires à chaque essai successif
            Incremental retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            // Active la stratégie de réitération pour les pannes concernant SQL Azure
            SqlAzureRetry = new RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy>(retryStrategy);
        } 
        #endregion

        /// <summary>
        /// Contexte EF
        /// </summary>
        public EscarGoContext Context { get; set; }

        /// <summary>
        /// Politique de réitération pour SQL Azure
        /// </summary>
        protected RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy> SqlAzureRetry { get; set; }

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
