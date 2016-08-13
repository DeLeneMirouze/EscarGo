using System;

namespace EscargoDisjoncteur.Models
{
    public interface ICircuitBreakerStateStore
    {
        bool IsClosed { get; }
        bool HasTimeoutCompleted();
        void SetHalfOpen();
        void Trip(Exception ex);
        Exception LastException { get; }
        void CloseCircuitBreaker();
    }
}