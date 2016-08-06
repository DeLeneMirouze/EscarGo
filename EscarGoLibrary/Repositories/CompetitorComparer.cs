using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public class CompetitorComparer : IEqualityComparer<Concurrent>
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
