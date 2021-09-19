using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Data
{
    /// <summary>
    /// A repository generic interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task LoadDataAsync();
    }
}
