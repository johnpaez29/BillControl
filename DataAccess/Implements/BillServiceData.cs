using DataAccess.Interfaces;
using DataAccess.Models;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implements
{
    public class BillServiceData : BaseData, IServiceData<BillData>, IServiceBillData<BillData>
    {
        public async Task Delete(string id)
        {
            await FirebaseClient.Child("Bills").Child(id).DeleteAsync();
        }

        public async Task<IEnumerable<BillData>> GetAllAsync()
        {
            var dbBills = await FirebaseClient.Child("Bills").OnceAsync<BillData>();

            IEnumerable<BillData> bills = dbBills.ToList().Select(dbBill => new BillData
            {
                Id = dbBill.Key,
                IdBill = dbBill.Object.IdBill,
                Name = dbBill.Object.Name,
                PayDate = dbBill.Object.PayDate,
                IdUser = dbBill.Object.IdUser,
            });

            return bills;
        }

        public async Task<IEnumerable<BillData>> GetAllByIdBillAsync(string id)
        {
            var dbBills = await FirebaseClient.Child("Bills").OnceAsync<BillData>();

            IEnumerable<BillData> bills = dbBills.Where(bill => bill.Object.IdBill == id).Select(dbBill => new BillData
            {
                Id = dbBill.Key,
                IdBill = dbBill.Object.IdBill,
                Name = dbBill.Object.Name,
                PayDate = dbBill.Object.PayDate,
                IdUser = dbBill.Object.IdUser,
                State = dbBill.Object.State
            });


            return bills;
        }

        public async Task<IEnumerable<BillData>> GetAllByIdUserAsync(string id)
        {
            var dbBills = await FirebaseClient.Child("Bills").OnceAsync<BillData>();

            IEnumerable<BillData> bills = dbBills.Where(bill => bill.Object.IdUser == id).Select(dbBill => new BillData
            {
                Id = dbBill.Key,
                IdBill = dbBill.Object.IdBill,
                Name = dbBill.Object.Name,
                PayDate = dbBill.Object.PayDate,
                IdUser = dbBill.Object.IdUser,
                State = dbBill.Object.State
            });

            return bills;
        }

        public async Task<BillData> GetById(string id)
        {
            var dbBill = await FirebaseClient.Child("Bills").Child(id).OnceSingleAsync<BillData>();

            BillData bill = new BillData
            {
                Id = id,
                Name = dbBill.Name
            };

            return bill;
        }

        public async Task InsertOne(BillData element)
        {
            await FirebaseClient.Child("Bills").PostAsync(element);
        }

        public async Task Update(BillData Element)
        {
            string id = Element.Id;
            Element.Id = null;
            await FirebaseClient.Child("Bills").Child(id).PutAsync(Element);

        }
    }
}
