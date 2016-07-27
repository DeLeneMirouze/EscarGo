using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.Repositories.CQRS
{
    public class RaceComparer : IEqualityComparer<Course>
    {
 

        public bool Equals(Course x, Course y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.CourseId == y.CourseId;
        }

        public int GetHashCode(Course obj)
        {
            return obj.CourseId.GetHashCode();
        }
    }
}
