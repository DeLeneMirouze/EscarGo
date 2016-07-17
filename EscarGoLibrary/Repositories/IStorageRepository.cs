using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.Repositories
{
    public interface IStorageRepository
    {
        List<Concurrent> GetCompetitors(string competitorId);
        List<Course> GetCourses(string competitorId);
    }
}