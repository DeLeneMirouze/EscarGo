using System;
using System.Threading;

namespace EscargoDisjoncteur.Models
{
    /// <summary>
    /// Implémente la gestion des états du disjocteur
    /// </summary>
    public class CircuitBreakerStateStore: ICircuitBreakerStateStore
    {
        #region Constructeur
        public CircuitBreakerStateStore(TimeSpan openToHalfOpenWaitTime, int maxTry)
        {
            State = CircuitBreakerStateEnum.Closed;
            _openToHalfOpenWaitTime = openToHalfOpenWaitTime;
            _maxTry = maxTry;
        }
        #endregion

        #region Proprités
        /// <summary>
        /// Nombre max de passage avant de basculer de HalfOpen vers Closed
        /// </summary>
        private int _maxTry;
        private int _currentTry;
        /// <summary>
        /// Etat du disjoncteur
        /// </summary>
        protected CircuitBreakerStateEnum State { get; set; }
        /// <summary>
        /// Date UTC de la dernière exception levée par le service
        /// </summary>
        protected DateTime LastStateChangedDateUtc { get; set; }
        /// <summary>
        /// Pour les locks
        /// </summary>
        private readonly object _padLock = new object();
        /// <summary>
        /// Durée où l'on reste en halfOpen
        /// </summary>
        protected readonly TimeSpan _openToHalfOpenWaitTime = new TimeSpan(0, 0, 60);
        /// <summary>
        /// Dernière exception levée par le service
        /// </summary>
        public Exception LastException { get; protected set; } 
        #endregion

        #region IsClosed
        public bool IsClosed
        {
            get
            {
                return State == CircuitBreakerStateEnum.Closed;
            }
        }
        #endregion

        #region HasTimeoutCompleted
        public bool HasTimeoutCompleted()
        {
            return (DateTime.UtcNow >= LastStateChangedDateUtc.Add(_openToHalfOpenWaitTime));
        }
        #endregion

        #region SetHalfOpen
        public void SetHalfOpen()
        {
            State = CircuitBreakerStateEnum.HalfOpen;
        }
        #endregion

        #region CloseCircuitBreaker (virtual)
        /// <summary>
        /// Ferme le disjoncteur
        /// </summary>
        public virtual void CloseCircuitBreaker()
        {
            lock (_padLock)
            {
                Interlocked.Increment(ref _currentTry);

                if (_currentTry > _maxTry)
                {

                    if (_currentTry > _maxTry)
                    {
                        _currentTry = 0;
                        State = CircuitBreakerStateEnum.Closed;
                    }
                }
            }
        }
        #endregion

        #region Trip
        /// <summary>
        /// Ouvre le disjoncteur suite à une exception et lève la-dite exception
        /// </summary>
        /// <param name="ex"></param>
        public void Trip(Exception ex)
        {
            lock (_padLock)
            {
                LastStateChangedDateUtc = DateTime.UtcNow;
                LastException = ex;
                State = CircuitBreakerStateEnum.Open;
                _currentTry = 0;
            }
        }
        #endregion
    }
}