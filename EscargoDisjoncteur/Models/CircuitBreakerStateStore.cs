using System;

namespace EscargoDisjoncteur.Models
{
    /// <summary>
    /// Implémente la logique applicative du pattern
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

        private int _maxTry { get; set; }
        private int _currentTry { get; set; }
        /// <summary>
        /// Etat du disjoncteur
        /// </summary>
        protected CircuitBreakerStateEnum State { get; set; }
        /// <summary>
        /// Date UTC de la dernière exception levée par le service
        /// </summary>
        protected DateTime LastStateChangedDateUtc { get; set; }
        private readonly object _padLock = new object();
        /// <summary>
        /// Durée où l'on reste en halfOpen
        /// </summary>
        protected readonly TimeSpan _openToHalfOpenWaitTime = new TimeSpan(0, 0, 60);
        /// <summary>
        /// Dernière exception levée par le service
        /// </summary>
        public Exception LastException { get; protected set; }

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

        #region HalfOpen
        public void HalfOpen()
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
            _currentTry++;

            if (_currentTry > _maxTry)
            {
                _currentTry = 0;
                State = CircuitBreakerStateEnum.Closed;
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
            }

            //throw new Exception("Disjoncteur ouvert", lastException);
        }
        #endregion
    }
}