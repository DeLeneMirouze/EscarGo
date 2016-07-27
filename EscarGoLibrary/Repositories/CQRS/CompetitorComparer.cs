using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.Repositories.CQRS
{
    public class CompetitorComparer: IEqualityComparer<Concurrent>
    {
        public bool Equals(Concurrent x, Concurrent y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.ConcurrentId == y.ConcurrentId;
        }

        public int GetHashCode(Concurrent obj)
        {
            return obj.ConcurrentId.GetHashCode();
        }
    }
}
