using System;

namespace EscargoDisjoncteur.Models
{
    public interface ICircuitBreakerStateStore
    {
        bool IsClosed { get; }
        bool HasTimeoutCompleted();
        void HalfOpen();
        void Trip(Exception ex);
        Exception LastException { get; }
        void CloseCircuitBreaker();
    }
}