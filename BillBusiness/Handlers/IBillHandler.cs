using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillBusiness.Handlers
{
    public interface IBillHandler
    {
        Task<IEnumerable<BillControl>> GetBills();

        void InsertBill(BillControl bill);
    }
}
