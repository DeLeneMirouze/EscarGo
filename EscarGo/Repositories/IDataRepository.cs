using EscarGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscarGo.Repositories
{
    public interface IDataRepository: IDisposable
    {
        List<Concurrent> GetConcurrents();
        Concurrent GetConcurrentById(int id);
    }
}
