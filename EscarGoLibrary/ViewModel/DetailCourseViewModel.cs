
using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.ViewModel
{
    public class DetailCourseViewModel
    {
        public Course Course { get; set; }
        public List<Concurrent> Concurrents { get; set; }
    }
}
