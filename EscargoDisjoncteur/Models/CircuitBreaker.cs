#region using
using System;
using System.Threading.Tasks;
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
                if (_circuitBreakerStateStore.HasTimeoutCompleted())
                {
                    // on peut tenter de passer en HalfOpen
                    try
                    {
                        _circuitBreakerStateStore.HalfOpen();

                        action();

                        // implémente la logique qui décide si on ouvre complètement le disjoncteur
                        _circuitBreakerStateStore.CloseCircuitBreaker();
                        return;
                    }
                    catch (Exception ex)
                    {
                        // encore une exception, on attent encore un peu pour ouvrir
                        _circuitBreakerStateStore.Trip(ex);
                        throw;
                    }
                }

                // permet au code client de savoir que le disjoncteur est ouvert et ainsi savoir s'il doit lancer une logique
                // spécifique
                throw new CircuitBreakerOpenException(_circuitBreakerStateStore.LastException);
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

                // rejoue l'exception pour que l'appelant sache à quoi on a affaire
                throw;
            }
        }
        #endregion

        #region ExecuteAsync
        /// <summary>
        /// Execute l'action passée en paramètre si le disjoncteur est ouvert
        /// </summary>
        /// <param name="action">Action à protéger par le disjoncteur</param>
        /// <returns></returns>
        public async Task ExecuteAsync(Func<Task> action)
        {
            if (!_circuitBreakerStateStore.IsClosed)
            {
                // le disjoncteur est ouvert
                if (_circuitBreakerStateStore.HasTimeoutCompleted())
                {
                    // on peut tenter de passer en HalfOpen
                    try
                    {
                        _circuitBreakerStateStore.HalfOpen();

                        await action();

                        // implémente la logique qui décide si on ouvre complètement le disjoncteur
                        _circuitBreakerStateStore.CloseCircuitBreaker();
                        return;
                    }
                    catch (Exception ex)
                    {
                        // encore une exception, on attent encore un peu pour ouvrir
                        _circuitBreakerStateStore.Trip(ex);
                        throw;
                    }
                }

                // permet au code client de savoir que le disjoncteur est ouvert et ainsi savoir s'il doit lancer une logique
                // spécifique
                throw new CircuitBreakerOpenException(_circuitBreakerStateStore.LastException);
            }


            // disjoncteur fermé, on exécute l'action
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                // on a une exception, on ouvre immédiatement le disjoncteur
                _circuitBreakerStateStore.Trip(ex);

                // rejoue l'exception pour que l'appelant sache à quoi on a affaire
                throw;
            }
        }
        #endregion
    }
}