using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillBusiness.Interfaces
{
    public interface IBillHandler
    {
        Task DeleteBill(string id);
        Task<IEnumerable<BillData>> GetBills();
        Task<IEnumerable<BillData>> GetBillsByIdUser(string id);
        Task<string> InsertBill(BillControl bill);
        Task UpdateBill(UpdateBill bill);
    }
}
