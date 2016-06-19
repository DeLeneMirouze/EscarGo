using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetCourses();
        Course GetCourseById(int id);
        List<Concurrent> GetConcurrentsByCourse(int idCourse);
        void Create(Course course);
        void Like(int idCourse);
    }
}
