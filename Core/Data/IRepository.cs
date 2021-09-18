using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task LoadDataAsync();
    }
}
