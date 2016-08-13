using System;
using System.Runtime.Serialization;

namespace EscargoDisjoncteur.Models
{
    public class CircuitBreakerOpenException : Exception, ISerializable
    {
        public CircuitBreakerOpenException()
        {

        }

        public CircuitBreakerOpenException(string message) : base(message)
        {

        }

        public CircuitBreakerOpenException(Exception exception) : base("Disjoncteur ouvert", exception)
        {

        }
    }
}