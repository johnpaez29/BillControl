using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IServiceBillData<T>
    {
        Task<IEnumerable<T>> GetAllByIdUserAsync(string id);
        Task<IEnumerable<BillData>> GetAllByIdBillAsync(string id);
    }
}
