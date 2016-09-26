using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIvecoding_uwp.Models
{
    public interface IRepository
    {
        IEnumerable<T> Get<T>() where T : class, new();
        T GetById<T>(int id) where T : class, new();
    }
}
