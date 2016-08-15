#region using
using System;
#endregion

namespace EscargoDisjoncteur.Models
{
    /// <summary>
    /// Implémente le pattern disjoncteur
    /// </summary>
    public sealed class CircuitBreaker
    {
        #region Constructeur
        readonly ICircuitBreakerStateStore _circuitBreakerStateStore;
        private readonly object _padLock = new object();

        public CircuitBreaker(ICircuitBreakerStateStore circuitBreakerStateStore)
        {
            _circuitBreakerStateStore = circuitBreakerStateStore;
        }
        #endregion

        #region Execute
        /// <summary>
        /// Execute l'action passée en paramètre si le disjoncteur est ouvert
        /// </summary>
        /// <param name="action">Action à protéger par le disjoncteur</param>
        public void Execute(Action action)
        {
            if (!_circuitBreakerStateStore.IsClosed)
            {
                // le disjoncteur est ouvert
                // on va tenter de l'entrouvrir

                lock (_padLock)
                {
                    if (!_circuitBreakerStateStore.IsClosed)
                    {
                        // le disjoncteur est ouvert
                        if (_circuitBreakerStateStore.HasTimeoutCompleted())
                        {
                            // on peut tenter de passer en HalfOpen
                            try
                            {

                                _circuitBreakerStateStore.SetHalfOpen();

                                action();

                                // implémente la logique qui décide si on ouvre complètement le disjoncteur
                                _circuitBreakerStateStore.CloseCircuitBreaker();

                                return;
                            }
                            catch (Exception ex)
                            {
                                // encore une exception, on attend encore un peu pour ouvrir

                                // faire un log ici ...

                                _circuitBreakerStateStore.Trip(ex);
                                throw new CircuitBreakerOpenException(_circuitBreakerStateStore.LastException);
                            }
                        }

                        // permet au code client de savoir que le disjoncteur est ouvert et ainsi savoir s'il doit lancer une logique
                        // spécifique

                        // faire un log ici ...

                        throw new CircuitBreakerOpenException(_circuitBreakerStateStore.LastException);
                    }
                }
            }


            // disjoncteur fermé, on exécute l'action
            try
            {
                action();
            }
            catch (Exception ex)
            {
                // on a une exception, on ouvre immédiatement le disjoncteur
                _circuitBreakerStateStore.Trip(ex);

                // Remplace l'exception par CircuitBreakerOpenException pour que le client sache que le disjoncteur est maintenant ouvert
                throw new CircuitBreakerOpenException(ex);
            }
        }
        #endregion
    }
}