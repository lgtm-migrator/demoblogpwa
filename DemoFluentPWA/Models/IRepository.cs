using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoFluentPWA.Models
{
    public interface IRepository<T> where T : class
    {
        Task InsertOneAsync(T item);
        Task InsertManyAsync(IEnumerable<T> items);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(string id);
        Task<bool> DeleteOneAsync(string id);
    }
}
