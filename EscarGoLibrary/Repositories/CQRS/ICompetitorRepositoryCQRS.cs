using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.Repositories.CQRS
{
    public interface ICompetitorRepositoryCQRS
    {
        List<Concurrent> GetCompetitors();
    }
}
