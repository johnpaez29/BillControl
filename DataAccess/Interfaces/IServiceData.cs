using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IServiceData<T>
    {
        Task<T> GetById(string id);

        Task<IEnumerable<T>> GetAllAsync();

        Task InsertOne(T element);

        Task Update(T Element);

        Task Delete(string id);
    }
}
