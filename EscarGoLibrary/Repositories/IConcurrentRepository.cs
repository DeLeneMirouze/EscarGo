using EscarGo.Models;
using System;
using System.Collections.Generic;

namespace EscarGo.Repositories
{
    public interface IConcurrentRepository: IDisposable
    {
        List<Concurrent> GetConcurrents();
        Concurrent GetConcurrentById(int id);
        List<Course> GetCoursesByConcurrent(int id);
        List<Pari> GetParisByConcurrent(int id);
        void SetBet(int idCourse, int idConcurrent);
        List<Pari> GetParisByCourse(int idCourse);
    }
}
