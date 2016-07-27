
using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.ViewModel
{
    public class DetailConcurrentViewModel
    {
        public DetailConcurrentViewModel()
        {
            Courses = new List<Course>();

        }
        public List<Course> Courses { get; set; }
        public Concurrent Concurrent { get; set; }
    }
}
